using System;
using Newtonsoft.Json;

namespace emerge.Data
{
    public class NewsAlert
    {
        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "updates")]
        public UpdateInfo[] Updates { get; set; }
    }
}