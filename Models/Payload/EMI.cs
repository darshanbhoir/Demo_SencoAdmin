using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Demo_Senco_Admin.Models.Payload
{
    public class EMI
    {
        [JsonProperty("$id")]
        public string id { get; set; }

        [DisplayFormat(NullDisplayText = "")]
        public string CANCELLEDBY { get; set; } 

        public string INSTALLMENTMONTH { get; set; }

        public string RECEIPTID { get; set; }

        public string CANCELLEDREASON { get; set; }

        public string TENDERTYPE { get; set; }

        public string TRANSACTIONID { get; set; }

        public string SCHEMEENTRYNO { get; set; }

        public string PAYMENTYEAR { get; set; }

        public string PAYMENTTYPE { get; set; }

        public string PAYMENTMONTH { get; set; }

        public string PAYMENTENTRYNO { get; set; }

        public string PAYMENTDATE { get; set; }

        public string PAYMENTAMOUNT { get; set; }

        public string LINENUM { get; set; }

        public string CUSTOMERCODE { get; set; }

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