using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    public class NewSchemePayload
    {
        [JsonProperty("_keys")]
        public List<NewScheme> Keys { get; set; }

        
        //public static implicit operator NewSchemePayload(List<NewSchemePayload> v)
        //{
        //    var Keys = new NewSchemePayload();
        //    return Keys;
        //    //throw new NotImplementedException();
        //}
    }
}