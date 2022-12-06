using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace emerge_azure.Models
{
    public class Department
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "alertList")]
        public List<NewsAlert> AlertList { get; set; }
        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }
    }
}
