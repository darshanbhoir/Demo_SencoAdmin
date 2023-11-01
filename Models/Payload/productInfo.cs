using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    [JsonObject]
    public class productInfo
    {
        public List<items> items { get; set; } 
    }
}