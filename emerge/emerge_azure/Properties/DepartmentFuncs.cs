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

public static class DeptFuncs
{
    public static readonly string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";

    [FunctionName("get_depts")]
    public static async Task<IActionResult> GetDepts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "depts")] HttpRequest req, // is Route correct? how to setup, ask teammates
        ILogger log)
    {
        List<Department> deptList = new List<Department>();
        log.LogInformation("Called get_dept with GET request");
        MongoClient dbClient = new MongoClient(connectionString);
        var database = dbClient.GetDatabase("emerge");
        var collection = database.GetCollection<BsonDocument>("databases");
        var documents = collection.AsQueryable();
        foreach (BsonDocument dept in documents)
        {
            var department = new Department();
            if (!dept["_id"].IsBsonNull)
            {
                department.Id = dept["_id"].AsString;
            }
            if (!dept["title"].IsBsonNull)
            {
                department.Title = dept["title"].AsString;
            }
            if (!dept["alertList"].IsBsonNull)
            {
                department.AlertList = new List<NewsAlert>();// dept["alertList"].AsBsonArray; // AlertList to be implemented later
            }
            if (!dept["imageurl"].IsBsonNull)
            {
                department.ImageUrl = dept["imageurl"].AsString;
            }


            deptList.Add(department);
        }
        Console.Write("departments populated");
        return new OkObjectResult(deptList);
    }

}