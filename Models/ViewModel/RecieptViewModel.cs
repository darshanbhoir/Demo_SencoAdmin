using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Demo_Senco_Admin.Models.ViewModel
{
    public class RecieptViewModel
    {
        public string RecieptId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string Store { get; set; }
        public string CINno { get; set; }
        public string SchemeNo { get; set; }
        public string SchemeCode { get; set; }
        public decimal EmiAmount { get; set; }
        public int EmiNo { get; set; }
    }
}