using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UserDatabase.Models
{
	public class Profile
	{
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
	}
}

