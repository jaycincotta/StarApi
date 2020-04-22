using System;
using System.IO;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace StarApi
{
    public static class PollGetNextEmail
    {
        [FunctionName("PollGetNextEmail")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            WebRequest request = WebRequest.Create("https://star.ipo.vote/api/v1/getnextemail/");
            WebResponse response = request.GetResponse();
            log.LogInformation(((HttpWebResponse)response).StatusDescription);

            using Stream dataStream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            log.LogInformation(responseFromServer);
            response.Close();
        }
    }
}
