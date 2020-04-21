using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Dapper;

namespace StarApi
{
    public static class FindVoterFunctions
    {
        [FunctionName(nameof(FindVoter))]
        public static async Task<IActionResult> FindVoter(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string firstName = req.Query["firstName"];
            string lastName = req.Query["lastName"];
            int birthYear = int.Parse(req.Query["birthYear"]);
            log.LogInformation($"{nameof(FindVoter)}: FirstName: {firstName} LastName: {lastName} BirthYear: {birthYear}");

            try
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE");
                log.LogInformation($"{nameof(FindVoter)}: connectionString length = {connectionString.Length}");
                using var connection = new SqlConnection(connectionString);
                var matches = await connection.QueryAsync<dynamic>(
                    "SELECT  top 100 VoterId, FirstName, LastName, Address1, HouseNum, StreetName, City, County, ZipCode from dbo.EligibleVoters WHERE FirstName = @firstName AND LastName = @lastName AND BirthYear = @birthYear",
                    new { firstName, lastName, birthYear }
                    ).ConfigureAwait(false);

                return new OkObjectResult(matches);
            }
            catch (Exception ex)
            {
                log.LogError($"{nameof(FindVoter)} FAILED", ex.Message, ex.GetType().Name, ex.StackTrace.Length, ex.InnerException.ToString());
                return new BadRequestObjectResult(ex.ToString());
            }
        }
    }
}
