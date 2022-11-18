using System;
using Newtonsoft.Json;



namespace emerge.Data
{
    public class AlertInfo
    {
        private DateTime time;
        private String title;
        private String author;
        private String description;
        private UpdateInfo[] updates;
        private int priority;
        private String imageUrl;

        public AlertInfo(DateTime time, string title, string author, string description, int priority, UpdateInfo[] updates, string imageUrl)
        {
            this.Time = time;
            this.Title = title;
            this.Author = author;
            this.Description = description;
            this.Updates = updates;
            this.Priority = priority;
            this.imageUrl = imageUrl;
        }
        public string ID { get; set; } = Guid.NewGuid().ToString("n");


        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get => time; set => time = value; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get => title; set => title = value; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get => author; set => author = value; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get => description; set => description = value; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get => priority; set => priority = value; }
        [JsonProperty(PropertyName = "updates")]
        public UpdateInfo[] Updates { get => updates; set => updates = value; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}

