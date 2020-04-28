using System;
using System.IO;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarApi.SendEmail;

namespace StarApi
{
    public static class PollGetNextEmail
    {
        private static CredentialCache GetCredential()
        {
            string url = @"https://star.ipo.vote/user/ajaxlogin/";
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential("api", "PCj4DsvyzADn9rY"));
            return credentialCache;
        }

        [FunctionName("PollGetNextEmail")]
        public static async void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            CookieContainer cookies = StarLogin.Login();
            bool done = false;

            while (!done)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://star.ipo.vote/api/v1/getnextemail/");
                    request.CookieContainer = cookies;
 
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
                        EmailTemplate template = EmailTemplate.Factory(emailRequest.template, emailRequest.fields);
                        log.LogInformation($"{emailRequest.template}: {template.Subject} to {template.ToEmail}");
                        await EmailTemplate.Send(template).ConfigureAwait(false);
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
