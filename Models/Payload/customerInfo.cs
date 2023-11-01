using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    [JsonObject]
    public class customerInfo
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
    }
}