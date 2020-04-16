using System;
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

namespace StarApi
{
    public static class RequestBallotFunctions
    {
        [FunctionName(nameof(RequestBallot))]
        public static async Task<IActionResult> RequestBallot(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(RequestBallot)}: Sending test email");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            log.LogInformation(requestBody);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("support@equal.vote", "Customer Support");
            var subject = "Test Ballot Link 3";
            var to = new EmailAddress("jay.cincotta@gmail.com", "Jay Cincotta");
            var plainTextContent = "https://staging.ipo.vote/ipo2020/s9n9nwpc54/";
            var htmlContent = "<a href='https://staging.ipo.vote/ipo2020/s9n9nwpc54/'>Ballot link</a>" +
                requestBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var responseMessage = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return new OkObjectResult(responseMessage);
        }
    }
}
