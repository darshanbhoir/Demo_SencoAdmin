using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    public class EMIPayload
    {
        [JsonProperty("$id")]
        public string id { get; set; }

        
        public string Status { get; set; }

        
        public string Message { get; set; }
        
        public List<EMI> EMI { get; set; }

        //public static implicit operator string(EMIPayload v)
        //{
        //    throw new NotImplementedException();
        //}


       public EMIPayload()
        {
            
            
            Message = "-";
            Status = "-";

        }

        public static implicit operator EMIPayload(List<EMIPayload> v)
        {
            throw new NotImplementedException();
        }
    }
}