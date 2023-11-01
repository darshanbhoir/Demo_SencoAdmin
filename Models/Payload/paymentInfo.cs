using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    [JsonObject]
    public class paymentInfo
    {
        public string oid { get; set; }
        public string status { get; set; }
        public string amount { get; set; }
        public string timestamp { get; set; }
    }
}