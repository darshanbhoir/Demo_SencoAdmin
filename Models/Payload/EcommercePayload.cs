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
        public productInfo productInfo { get; set; }

        [JsonProperty("customerInfo")]
        public customerInfo customerInfo { get; set; }

        [JsonProperty("paymentInfo")]
        public paymentInfo paymentInfo { get; set; }

        //public static implicit operator EcommercePayload(List<EcommercePayload> v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}