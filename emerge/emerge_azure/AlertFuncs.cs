using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using emerge_azure.Models;
using Newtonsoft.Json;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

public static class AlertFuncs
{
    [FunctionName("get_newsalert")]
    public static async Task<IActionResult> GetNewsAlerts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "newsalert")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Called get_newsalert with GET request");

        return new OkObjectResult(NewsAlertFeed.newsalerts);
    }

    [FunctionName("add_newsalert")]
    public static async Task<IActionResult> AddNewsAlert(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "newsalert")] HttpRequest req,
            ILogger log)
    {
        var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString"));
        var database = client.GetDatabase("alerts");
        var collection = database.GetCollection<NewsAlert>("alertInfo");
        ////We could also just drop the collection
        //await collection.DeleteManyAsync(new BsonDocument { });
        //await collection.Indexes.DropAllAsync();
        //njnjnjnj

        log.LogInformation("Called add_newsalert with POST request");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var newsalert = JsonConvert.DeserializeObject<NewsAlert>(requestBody);

        await collection.InsertOneAsync(newsalert);

        return new OkObjectResult(newsalert);
    }
}