using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StarApi
{
    public static class Stub1
    {
        [FunctionName(nameof(Ping))]
        public static async Task<IActionResult> Ping(
            [HttpTrigger(AuthorizationLevel.Function, "post", "options", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Ping!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            log.LogInformation(requestBody);
            //var data = "HELLO";
            return new OkObjectResult(data);
        }
    }
}
