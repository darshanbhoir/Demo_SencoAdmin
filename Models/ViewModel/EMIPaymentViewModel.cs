using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.ViewModel
{
    public class EMIPaymentViewModel
    {
        public string OrderNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? EMIno { get; set; }
        public decimal Amount { get; set; }
        public bool? PaymentStatus { get; set; }
        public int SchemeNo { get; set; }
        public string TransactionId { get; set; }
        public string BankTransactionId { get; set; }
        public string PaymentEntryNo { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PaymentReciept { get; set; }

    }
}