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
    public static class VoterFunctions
    {
        [FunctionName(nameof(FindByName))]
        public static async Task<IActionResult> FindByName(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string firstName = req.Query["firstName"];
            string lastName = req.Query["lastName"];
            int birthYear = int.Parse(req.Query["birthYear"]);
            log.LogInformation($"{nameof(FindByName)}: FirstName: {firstName} LastName: {lastName} BirthYear: {birthYear}");

            var connectionString = Environment.GetEnvironmentVariable("VoterDb");
            using var connection = new SqlConnection(connectionString);
            var matches = await connection.QueryAsync<dynamic>(
                "SELECT  top 100 VoterId, FirstName, LastName, Address1, HouseNum, StreetName, City, County, ZipCode from VOTERS WHERE FirstName = @firstName AND LastName = @lastName AND BirthYear = @birthYear",
                new { firstName, lastName, birthYear }
                ).ConfigureAwait(false);

            return new OkObjectResult(matches);
        }

        [FunctionName(nameof(SearchByName))]
        public static async Task<IActionResult> SearchByName(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string firstName = req.Query["firstName"] + "%";
            string lastName = req.Query["lastName"] + "%";
            int birthYear = int.Parse(req.Query["birthYear"]);
            log.LogInformation($"{nameof(SearchByName)}: FirstName: {firstName} LastName: {lastName} BirthYear: {birthYear}");

            var connectionString = Environment.GetEnvironmentVariable("VoterDb");
            using var connection = new SqlConnection(connectionString);
            var matches = await connection.QueryAsync<dynamic>(
                "SELECT top 100 VoterId, FirstName, LastName, Address1, HouseNum, StreetName, City, County, ZipCode from VOTERS WHERE FirstName LIKE @firstName AND LastName LIKE @lastName AND BirthYear = @birthYear",
                new { firstName, lastName, birthYear }
                ).ConfigureAwait(false);

            return new OkObjectResult(matches);
        }

        [FunctionName(nameof(FindByAddress))]
        public static async Task<IActionResult> FindByAddress(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            int zipcode = int.Parse(req.Query["zipcode"]);
            int houseNum = int.Parse(req.Query["houseNum"]);
            int birthYear = int.Parse(req.Query["birthYear"]);
            log.LogInformation($"{nameof(FindByAddress)}: Zipcode: {zipcode} HouseNum: {houseNum} BirthYear: {birthYear}");

            var connectionString = Environment.GetEnvironmentVariable("VoterDb");
            using var connection = new SqlConnection(connectionString);
            var matches = await connection.QueryAsync<dynamic>(
                "SELECT top 100 VoterId, FirstName, LastName, Address1, HouseNum, StreetName, City, County, ZipCode  from VOTERS WHERE zipcode = @zipcode AND houseNum = @houseNum AND BirthYear = @birthYear",
                new { zipcode, houseNum, birthYear }
                ).ConfigureAwait(false);

            return new OkObjectResult(matches);
        }

        [FunctionName(nameof(SearchByAddress))]
        public static async Task<IActionResult> SearchByAddress(
[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            int zipcode = int.Parse(req.Query["zipcode"]);
            int houseNum = int.Parse(req.Query["houseNum"]);
            log.LogInformation($"{nameof(SearchByAddress)}: Zipcode: {zipcode} HouseNum: {houseNum}");

            var connectionString = Environment.GetEnvironmentVariable("VoterDb");
            using var connection = new SqlConnection(connectionString);
            var matches = await connection.QueryAsync<dynamic>(
                "SELECT VoterId, FirstName, LastName, Address1, HouseNum, StreetName, City, County, ZipCode from VOTERS WHERE zipcode = @zipcode AND houseNum = @houseNum",
                new { zipcode, houseNum }
                ).ConfigureAwait(false);

            return new OkObjectResult(matches);
        }

        [FunctionName(nameof(Ping2))]
        public static async Task<IActionResult> Ping2(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
    ILogger log)
        {
            log.LogInformation("Ping!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            //var data = "HELLO";
            return new OkObjectResult(data);
        }
    }
}
