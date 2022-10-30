using System;
namespace emerge.Data
{
    public class UpdateInfo
    {
        private DateTime dateTime;
        private String description;

        public UpdateInfo(DateTime dateTime, string description)
        {
            this.dateTime = dateTime;
            this.Description = description;
        }

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Description { get => description; set => description = value; }
    }
}

