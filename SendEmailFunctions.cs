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
    public static class RequestBallotFunctions
    {
        [FunctionName(nameof(RequestBallot))]
        public static async Task<IActionResult> RequestBallot(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation($"{nameof(RequestBallot)}");

                string body = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
                dynamic request = string.IsNullOrEmpty(body) ? CreateRequest(req.Query)
                    : JsonConvert.DeserializeObject(body);

                EmailTemplate template = EmailTemplate.Factory("ballotLink", request);
                if (template == null)
                {
                    log.LogInformation("Nothing to process");
                }
                else
                {
                    log.LogInformation($"{nameof(RequestBallot)}: {template.TemplateName}");
                    await EmailTemplate.Send(template).ConfigureAwait(false);
                    log.LogInformation($"{nameof(RequestBallot)} Succeeded");
                }
                return new OkObjectResult($"{nameof(RequestBallot)}: Succeeded");
            }
            catch (Exception ex)
            {
                log.LogError($"{nameof(RequestBallot)} FAILED", ex.Message, ex.GetType().Name, ex.StackTrace.Length, ex.InnerException.ToString());
                return new BadRequestObjectResult(ex.ToString());
            }
        }

        static string Denullify(string arg) => string.IsNullOrEmpty(arg) ? "" : arg.ToString();

        private static object CreateRequest(IQueryCollection query)
        {
            dynamic parameters = new ExpandoObject();
            parameters.firstName = Denullify(query["firstName"]);
            parameters.lastName = Denullify(query["lastName"]);
            parameters.email = Denullify(query["email"]);
            parameters.ballotHtml = Denullify(query["ballotHtml"]);
            parameters.starId = Denullify(query["starId"]);
            return parameters;
        }
    }
}
