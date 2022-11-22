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
using MongoDB.Driver;
using System;
using MongoDB.Bson;
using System.Diagnostics;
using UserDatabase.Models;
using System.Collections.Generic;

public static class AlertFuncs
{
    public static readonly string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";

    [FunctionName("get_newsalert")]
    public static async Task<IActionResult> GetNewsAlerts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "newsalert")] HttpRequest req,
        ILogger log)
    {
        List<NewsAlert> alertList = new List<NewsAlert>();
        log.LogInformation("Called get_newsalert with GET request");
        MongoClient dbClient = new MongoClient(connectionString);
        var database = dbClient.GetDatabase("alerts");
        var collection = database.GetCollection<BsonDocument>("alertInfo");
        var documents = collection.AsQueryable();
        foreach (BsonDocument alertDoc in documents)
        {
            var alert = new NewsAlert();
            if (!alertDoc["Time"].IsBsonNull)
            {
                alert.Time = alertDoc["Time"].AsDateTime;
            }
            if (!alertDoc["Title"].IsBsonNull)
            {
                alert.Title = alertDoc["Title"].AsString;
            }
            if (!alertDoc["Author"].IsBsonNull)
            {
                alert.Author = alertDoc["Author"].AsString;
            }
            if (!alertDoc["Time"].IsBsonNull)
            {
                alert.Description = alertDoc["Description"].AsString;
            }
            if (!alertDoc["Priority"].IsBsonNull)
            {
                alert.Priority = (int) alertDoc["Priority"].AsInt32;
            }
            
            //deserialize update
            alertList.Add(alert);
        }

        return new OkObjectResult(alertList);
    }

    //[FunctionName("get_newsalert_byID")]
    //public static async Task<IActionResult> GetNewsAlertsById(
    //        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fooditem/{id}")] HttpRequest req,
    //        ILogger log, string id)
    //{
    //    log.LogInformation("Called get_newsalert_byID with GET request");

    //    var newsItem = NewsAlertFeed.newsalerts.FirstOrDefault(f => f.Id.Equals(id));
    //    if (newsItem == null)
    //    {
    //        return new NotFoundResult();
    //    }

    //    return new OkObjectResult(newsItem);
    //}

    [FunctionName("add_newsalert")]
    public static async Task<IActionResult> AddNewsAlert(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "newsalert")] HttpRequest req,
            ILogger log)
    {
        log.LogInformation("Called add_newsalert with POST request");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var newsAlert = JsonConvert.DeserializeObject<NewsAlert>(requestBody);

        NewsAlertFeed.newsalerts.Add(newsAlert);

        //add user to MongoDB
        string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
        string databaseName = "alerts";
        string collectionName = "alertInfo";

        // Establish connection to MongoDB.
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        var collection = db.GetCollection<NewsAlert>(collectionName);

        // Insert UserModel object into MongoDB.
        await collection.InsertOneAsync(newsAlert);


        // Locate document with Username field equal to "emerge"
        //var results = await collection.FindAsync(document => document.Username == "emerge");
        //Debug.WriteLine(results);

        return new OkObjectResult(newsAlert);

    }
}