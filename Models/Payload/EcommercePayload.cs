using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    public class EcommercePayload
    {
        [JsonProperty("productInfo")]
        public List<productInfo> productInfo { get; set; }
        [JsonProperty("customerInfo")]
        public List<customerInfo> customerInfo { get; set; }
        [JsonProperty("paymentInfo")]
        public List<paymentInfo> paymentInfo { get; set; }
    }
}