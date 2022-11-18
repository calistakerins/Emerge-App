using System;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace emerge_azure.Models
{

    public class UpdateInfo
    {
        //[BsonElement("updateDateTime")]
        //public DateTime dateTime { get; set; }
        [BsonElement("updateDesc")]
        public String description { get; set; }

    }

    public class NewsAlert
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Author")]
        public string Author { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Priority")]
        public int Priority { get; set; }
        [BsonElement("Updates")]
        public List<UpdateInfo> Updates { get; set; }
    }
}