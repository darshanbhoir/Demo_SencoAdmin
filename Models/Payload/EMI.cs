using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Demo_Senco_Admin.Models.Payload
{
    public class EMI
    {
        [JsonProperty("$id")]
        public string id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CANCELLEDBY { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string INSTALLMENTMONTH { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string RECEIPTID { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CANCELLEDREASON { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string TENDERTYPE { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string TRANSACTIONID { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string SCHEMEENTRYNO { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PAYMENTYEAR { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PAYMENTTYPE { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PAYMENTMONTH { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PAYMENTENTRYNO { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PAYMENTDATE { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PAYMENTAMOUNT { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string LINENUM { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CUSTOMERCODE { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CANCELLED { get; set; }


        public EMI()
        {
            CANCELLEDBY = "-";
            INSTALLMENTMONTH = "-";
            RECEIPTID = "-";
            CANCELLEDREASON = "-";
            TENDERTYPE = "-";
            TRANSACTIONID = "-";
            SCHEMEENTRYNO = "-";
            PAYMENTYEAR = "-";
            PAYMENTTYPE = "-";
            PAYMENTMONTH = "-";
            PAYMENTENTRYNO = "-";
            PAYMENTDATE = "-";
            PAYMENTAMOUNT = "-";
            LINENUM = "-";
            CUSTOMERCODE = "-";
            CANCELLED = "-";
            

        }
    }
}