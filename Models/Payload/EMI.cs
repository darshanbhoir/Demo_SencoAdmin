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

        [DisplayFormat(NullDisplayText = "")]
        public string INSTALLMENTMONTH { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string RECEIPTID { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string CANCELLEDREASON { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string TENDERTYPE { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string TRANSACTIONID { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string SCHEMEENTRYNO { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string PAYMENTYEAR { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string PAYMENTTYPE { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string PAYMENTMONTH { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string PAYMENTENTRYNO { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string PAYMENTDATE { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string PAYMENTAMOUNT { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string LINENUM { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string CUSTOMERCODE { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string CANCELLED { get; set; }


        //public EMI()
        //{
        //    CANCELLEDBY = "-";
        //    INSTALLMENTMONTH = "-";
        //    RECEIPTID = "-";
        //    CANCELLEDREASON = "-";
        //    TENDERTYPE = "-";
        //    TRANSACTIONID = "-";
        //    SCHEMEENTRYNO = "-";
        //    PAYMENTYEAR = "-";
        //    PAYMENTTYPE = "-";
        //    PAYMENTMONTH = "-";
        //    PAYMENTENTRYNO = "-";
        //    PAYMENTDATE = "-";
        //    PAYMENTAMOUNT = "-";
        //    LINENUM = "-";
        //    CUSTOMERCODE = "-";
        //    CANCELLED = "-";
        //}

    }
}