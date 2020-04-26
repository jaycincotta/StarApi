using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarApi.SendEmail;

namespace StarApi
{
    public static class PollGetNextEmail
    {
        [FunctionName("PollGetNextEmail")]
        public static async void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            bool done = false;

            while (!done)
            {
                try
                {
                    WebRequest request = WebRequest.Create("https://star.ipo.vote/api/v1/getnextemail/");
                    WebResponse response = request.GetResponse();

                    log.LogInformation(((HttpWebResponse)response).StatusDescription);

                    using Stream dataStream = response.GetResponseStream();
                    using StreamReader reader = new StreamReader(dataStream);
                    string body = reader.ReadToEnd();
                    log.LogInformation(body);
                    var emailRequest = JsonConvert.DeserializeObject<EmailRequest>(body);

                    if (emailRequest.isEmpty)
                    {
                        done = true;
                    }
                    else
                    {
                        EmailTemplate template = SendEmailFunctions.GetEmailTemplate(emailRequest.template, emailRequest.fields);
                        log.LogInformation($"{emailRequest.template}: {template.Subject} to {template.ToEmail}");
                        await SendEmailFunctions.SendEmailTemplate(template).ConfigureAwait(false);
                    }
                    response.Close();
                }
                catch (Exception ex)
                {
                    log.LogError($"{nameof(SendEmail)} FAILED", ex.Message, ex.GetType().Name, ex.StackTrace.Length, ex.InnerException.ToString());
                }
            }
        }

        private static void Validate(EmailRequest emailRequest)
        {
            if (emailRequest.apiVersion != "v1") throw new InvalidDataException($"Expected version v1, got: {emailRequest.apiVersion}");
        }
    }
}
