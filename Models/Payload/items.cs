using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    [JsonObject]
    public class items
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public data data { get; set; }
    }
}