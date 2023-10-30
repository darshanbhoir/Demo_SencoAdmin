﻿using Demo_Senco_Admin.Models.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.ViewModel
{
    public class EMIPaymentViewModel
    {
        public int YojnaId { get; set; }
        public string OrderNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? EMIno { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string SchemeNo { get; set; }
        //public string SchemeCode { get; set; }
        public string TransactionId { get; set; }
        public string BankTransactionId { get; set; }
        public string PaymentEntryNo { get; set; }
        //spublic string CustomerCode { get; set; }
        //public string StoreType { get; set; }
        public string CustomerName { get; set; }
        //public string CustomerAddress { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PaymentReciept { get; set; }
        //public string SchemeEntryNo { get; set; }
        public EMIPayload PayloadData { get; set; }

    }
}