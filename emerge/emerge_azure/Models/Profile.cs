using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace UserDatabase.Models
{
    [BsonIgnoreExtraElements]
    public class Profile
	{
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "admin")]
        public bool Admin { get; set; }
        [JsonProperty(PropertyName = "zipcode")]
        public string Zipcode { get; set; }
        [JsonProperty(PropertyName = "alerts")]
        public List<string> Alerts { get; set; }
        [JsonProperty(PropertyName = "following")]
        public List<string> Following { get; set; }
    }
}

