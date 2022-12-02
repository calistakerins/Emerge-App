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
using System.Collections.Generic;
using System.Linq;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using MongoDB.Bson.Serialization;

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
            if (!alertDoc["_id"].IsBsonNull)
            {
                alert.Id = alertDoc["_id"].AsString;
            }
            if (!alertDoc["Time"].IsBsonNull)
            {
                alert.Time = alertDoc["Time"].AsDateTime;
            }
            if (!alertDoc["Affected"].IsBsonNull)
            {
                List<string> affectedList = new List<string>();
                var affectedArr = alertDoc["Affected"].AsBsonArray;
                foreach (string zipcode in affectedArr)
                {
                    affectedList.Add(zipcode);
                }
                alert.Affected = affectedList;
            }
            if (!alertDoc["Title"].IsBsonNull)
            {
                alert.Title = alertDoc["Title"].AsString;
            }
            if (!alertDoc["Author"].IsBsonNull)
            {
                alert.Author = alertDoc["Author"].AsString;
            }
            if (!alertDoc["Department"].IsBsonNull)
            {
                alert.Department = BsonSerializer.Deserialize<Department>(alertDoc["Department"].ToJson());
            }
            if (!alertDoc["Description"].IsBsonNull)
            {
                alert.Description = alertDoc["Description"].AsString;
            }
            if (!alertDoc["Priority"].IsBsonNull)
            {
                alert.Priority = (int) alertDoc["Priority"].AsInt32;
            }
            if (!alertDoc["ImageUrl"].IsBsonNull)
            {
                alert.ImageUrl = alertDoc["ImageUrl"].AsString;
            }

            //access updates
            if (!alertDoc["Updates"].IsBsonNull)
            {
                List<UpdateInfo> updateList = new List<UpdateInfo>();
                var updates = alertDoc["Updates"].AsBsonArray;
                foreach (BsonDocument update in updates)
                {

  
                    UpdateInfo updateObj = new UpdateInfo();
                    if (!update["UpdateTime"].IsBsonNull)
                    {
                         updateObj.UpdateTime = update["UpdateTime"].AsDateTime;
                    }

                    if (!update["UpdateDescription"].IsBsonNull)
                    {
                        updateObj.UpdateDescription = update["UpdateDescription"].AsString;
                    }

                    updateList.Add(updateObj);
                }
                alert.Updates = updateList;

            }
            alertList.Add(alert);
        }
        Console.Write("hello");
        return new OkObjectResult(alertList);
    }

    [FunctionName("add_department")]
    public static async Task<IActionResult> AddNewsAlert(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "department")] HttpRequest req,
            ILogger log)
    {
        log.LogInformation("Called add_newsalert with POST request");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var department = JsonConvert.DeserializeObject<Department>(requestBody);

        DepartmentStore.dpts.Add(department);

        //add user to MongoDB
        string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
        string databaseName = "emerge";
        string collectionName = "databases";

        // Establish connection to MongoDB.
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        var collection = db.GetCollection<Department>(collectionName);

        // Insert UserModel object into MongoDB.
        await collection.InsertOneAsync(department);

        return new OkObjectResult(department);
    }

    [FunctionName("add_newsalert")]
    public static async Task<IActionResult> AddDepartment(
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
        return new OkObjectResult(newsAlert);

    }


        [FunctionName("update_newsalert")]
        public static async Task<IActionResult> UpdateFoodItem(
               [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "newsalert/{id}")] HttpRequest req,
               ILogger log, string id)
        {
            log.LogInformation("Called update_newsalert with get request.");

            string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "alerts";
            string collectionName = "alertInfo";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<NewsAlert>(collectionName);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var newsAlertUpdate = JsonConvert.DeserializeObject<UpdateInfo>(requestBody);

            var filterDef = Builders<NewsAlert>.Filter.Eq(f => f.Id, id);
            var updateDef = Builders<NewsAlert>.Update
                .Set(p => p.Priority, newsAlertUpdate.UpdatePriority)
                .Push(p => p.Updates, newsAlertUpdate);

            if (filterDef == null)
            {
                return new NotFoundResult();
            }
            NewsAlert newUpdates = collection.FindOneAndUpdate(filterDef, updateDef);
            newUpdates.Priority = newsAlertUpdate.UpdatePriority;
            newUpdates.Updates.Append(newsAlertUpdate);
            return new OkObjectResult(newUpdates);
        }
    



}

