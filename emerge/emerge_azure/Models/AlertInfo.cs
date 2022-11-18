using AzureAlerts;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;

namespace AzureAlerts.Models
{
    public class AlertInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("PLU")]
        public int PLU { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("Description")]
        public string Description{ get; set; }

        [BsonElement("Updates")]
        public UpdateInfo[] Updates { get; set; }

        [BsonElement("Priority")]
        public double Priority { get; set; }

        [BsonElement("ImageURL")]
        public string ImageURL { get; set; }


    }

    public class UpdateInfo
    {
        [BsonElement("DateTime")]
        public DateTime DateTime { get; set; }


        [BsonElement("Description")]
        public int Description { get; set; }
    }
}

