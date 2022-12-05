﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using emerge_azure.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using MongoDB.Driver;
using System.Diagnostics;
using MongoDB.Bson.Serialization.Attributes;

namespace UserDatabase
{
    public static class ProfileFuncs
    {
        //function to get list of all users in database
        [FunctionName("get_users")]
        public static async Task<IActionResult> GetUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "profile")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Called get_users with GET request");

            return new OkObjectResult(ProfileStore.users);
        }

        //function to get specific profile based on unique username
        [FunctionName("get_profile_by_username")]
        public static async Task<IActionResult> GetProfileByUsername(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "profile/{username}")] HttpRequest req,
            ILogger log, string username)
        {
            log.LogInformation("Called get_profile_by_username with GET request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            //add user to MongoDB
            string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "emerge";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Profile>(collectionName);

            //find user by username
            var filterDef = Builders<Profile>.Filter.Eq(p => p.Username, username);

            var user = collection.Find(filterDef).FirstOrDefault();

            return new OkObjectResult(user);
        }


        //function to get add profile to the database
        [FunctionName("add_user")]
        public static async Task<IActionResult> AddUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "profile")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Called add_user with POST request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var user = JsonConvert.DeserializeObject<Profile>(requestBody);

            ProfileStore.users.Add(user);

            //add user to MongoDB
            string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "emerge";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Profile>(collectionName);

            // Insert UserModel object into MongoDB.
            if (user.Username != null || user.Password != null || user.Email != null)
            {
                await collection.InsertOneAsync(user);
            }


            // Locate document with Username field equal to "emerge"
            var results = await collection.FindAsync(document => document.Username == "emerge");
            Debug.WriteLine(results);

            return new OkObjectResult(user);
        }

        [FunctionName("admin_post_alert")]
        public static async Task<IActionResult> UpdateFoodItem(
               [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "newpost/{username}")] HttpRequest req,
               ILogger log, string username)
        {
            log.LogInformation("Called admin_post_alert with patch request.");

            string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "emerge";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Profile>(collectionName);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var newsAlertUpdate = JsonConvert.DeserializeObject<NewsAlert>(requestBody);

            var filterDef = Builders<Profile>.Filter.Eq(f => f.Username, username);
            var updateDef = Builders<Profile>.Update
                .Push(p => p.Alerts, newsAlertUpdate);

            if (filterDef == null)
            {
                return new NotFoundResult();
            }

            Profile newUpdates = collection.FindOneAndUpdate(filterDef, updateDef);
            newUpdates.Alerts.Append(newsAlertUpdate);
            return new OkObjectResult(newUpdates);
        }



        [FunctionName("follow_dept")]
        public static async Task<IActionResult> FollowDept(
               [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "followdept/{username}")] HttpRequest req,
               ILogger log, string username)
        {
            log.LogInformation("Called follow_dept with patch request.");

            string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "emerge";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Profile>(collectionName);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var following = JsonConvert.DeserializeObject<Department>(requestBody);

            var filterDef = Builders<Profile>.Filter.Eq(f => f.Username, username);
            var updateDef = Builders<Profile>.Update
                .Push(p => p.Following, following);

            if (filterDef == null)
            {
                return new NotFoundResult();
            }

            Profile newUpdates = collection.FindOneAndUpdate(filterDef, updateDef);
            newUpdates.Following.Append(following);
            return new OkObjectResult(newUpdates);
        }


        [FunctionName("unfollow_dept")]
        public static async Task<IActionResult> UnfollowDept(
               [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "unfollowdept/{username}")] HttpRequest req,
               ILogger log, string username)
        {
            log.LogInformation("Called unfollow_dept with patch request.");

            string connectionString = "mongodb+srv://emerge:project3@cluster0.ztzvtkd.mongodb.net/?retryWrites=true&w=majority";
            string databaseName = "emerge";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Profile>(collectionName);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var following = JsonConvert.DeserializeObject<Department>(requestBody);

            var filterDef = Builders<Profile>.Filter.Eq(f => f.Username, username);  //finds the user by username
            var updateDef = Builders<Profile>.Update
                .Pull(p => p.Following, following); //pulls the department from the following list

            if (filterDef == null)
            {
                return new NotFoundResult();
            }

            Profile newUpdates = collection.FindOneAndUpdate(filterDef, updateDef); //updates the user
            newUpdates.Following.Remove(following);  //removes the department from the following list
            return new OkObjectResult(newUpdates);
        }


    }
}


