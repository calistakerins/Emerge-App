﻿using System;
using Newtonsoft.Json;

namespace emerge_azure.Models
{
    public class UpdateInfo
    {
        [JsonProperty(PropertyName = "updatetime")]
        public DateTime UpdateTime { get; set; }
        [JsonProperty(PropertyName = "updatedescription")]
        public string UpdateDescription { get; set; }
        [JsonProperty(PropertyName = "updatepriority")]
        public int UpdatePriority { get; set; }
    }
}

