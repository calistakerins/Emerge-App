using System;
namespace emerge.Data
{
    public class AlertInfo
    {
        private DateTime dateTime;
        private String title;
        private String author;
        private UpdateInfo[] updates;

        public AlertInfo(DateTime dateTime, string title, string author, UpdateInfo[] updates)
        {
            this.dateTime = dateTime;
            this.Title = title;
            this.Author = author;
            this.Updates = updates;
        }

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public UpdateInfo[] Updates { get => updates; set => updates = value; }
    }
}

