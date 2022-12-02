﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace emerge_azure.Models
{
    public class NewsAlert
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; set; }
        [JsonProperty(PropertyName = "affected")]
        public List<string> Affected { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "department")]
        public Department Department { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }
        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "updates")]
        public List<UpdateInfo> Updates { get; set; }
    }
}