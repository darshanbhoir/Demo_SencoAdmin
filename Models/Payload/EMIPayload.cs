using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    public class EMIPayload
    {
        public int id { get; set; }
        public string CANCELLEDBY { get; set; }
        public string INSTALLMENTMONTH { get;set; }
        public string RECEIPTID { get;set; }
        public string CANCELLEDREASON { get;set; }
        public string TENDERTYPE { get;set; }
        public string TRANSACTIONID { get;set;}
        public string SCHEMEENTRYNO { get;set; }  
        public string PAYMENTYEAR { get;set; }
        public string PAYMENTTYPE { get;set; }
        public string PAYMENTMONTH { get; set;} 
        public string PAYMENTENTRYNO { get; set; }
        public DateTime PAYMENTDATE { get;set; }
        public string PAYMENTAMOUNT { get; set; }
        public string LINENUM { get; set; }
        public string CUSTOMERCODE { get; set; }
        public string CANCELLED { get; set; }

        public static implicit operator string(EMIPayload v)
        {
            throw new NotImplementedException();
        }
    }
}