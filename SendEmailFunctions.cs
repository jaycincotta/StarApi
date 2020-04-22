using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using StarApi.SendEmail;
using StarApi.SendEmail.Templates;
using System.Dynamic;
using System.Collections.Generic;

namespace StarApi
{
    public static class SendEmailFunctions
    {
        [FunctionName(nameof(SendEmail))]
        public static async Task<IActionResult> SendEmail(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation($"{nameof(SendEmail)}");

                string body = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
                dynamic request = string.IsNullOrEmpty(body) ? CreateRequest(req.Query)
                    : JsonConvert.DeserializeObject(body);

                EmailTemplate template = null;
                switch (request.template)
                {
                    case "BallotLink":
                        template = new BallotLink(request);
                        break;
                    case "VoteReceipt":
                        template = new VoteReceipt(request);
                        break;
                    case null:
                        throw new InvalidDataException("Missing template name");
                    default:
                        throw new InvalidDataException($"Unknown template name: {request.template}");
                }

                log.LogInformation($"{nameof(SendEmail)}: {template.TemplateName}");

                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);

                var msg = MailHelper.CreateSingleEmail(template.From, template.To, template.Subject, template.Text, template.Html);

                log.LogInformation($"{nameof(SendEmail)} Before SendEmailAsync");
                var responseMessage = await client.SendEmailAsync(msg).ConfigureAwait(false);
                log.LogInformation($"{nameof(SendEmail)} Succeeded");
                return new OkObjectResult($"{nameof(SendEmail)}: Succeeded");
            }
            catch (Exception ex)
            {
                log.LogError($"{nameof(SendEmail)} FAILED", ex.Message, ex.GetType().Name, ex.StackTrace.Length, ex.InnerException.ToString());
                return new BadRequestObjectResult(ex.ToString());
            }
        }

        static string Denullify(string arg) => string.IsNullOrEmpty(arg) ? "" : arg.ToString();

        private static object CreateRequest(IQueryCollection query)
        {
            dynamic parameters = new ExpandoObject();
            parameters.template = Denullify(query["template"]);
            parameters.firstName = Denullify(query["firstName"]);
            parameters.lastName = Denullify(query["lastName"]);
            parameters.email = Denullify(query["email"]);
            parameters.ballotHtml = Denullify(query["ballotHtml"]);
            parameters.starId = Denullify(query["starId"]);
            return parameters;
        }
    }
}
