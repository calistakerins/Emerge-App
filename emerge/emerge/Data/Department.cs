using System;
using Newtonsoft.Json;


namespace emerge.Data
{
    public class Department
    {
        private String title;
        private String administrator;
        private String description;
        private List<AlertInfo> alertList;
        private String imageUrl;

        public Department(string title, string administrator, string description, List<AlertInfo> alertList, string imageUrl)
        {
            this.Title = title;
            this.Administrator = administrator;
            this.Description = description;
            this.AlertList = alertList;
            this.imageUrl = imageUrl;
        }
        public string ID { get; set; } = Guid.NewGuid().ToString("n");

        [JsonProperty(PropertyName = "title")]
        public string Title { get => title; set => title = value; }
        [JsonProperty(PropertyName = "administrator")]
        public string Administrator { get => administrator; set => administrator = value; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get => description; set => description = value; }
        [JsonProperty(PropertyName = "alertList")]
        public List<AlertInfo> AlertList { get => alertList; set => alertList = value; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}

