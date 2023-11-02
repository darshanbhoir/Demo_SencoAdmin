using Demo_Senco_Admin.Models.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.ViewModel
{
    public class EcommerceViewModel
    {
        public int Ecommerce_Id { get; set; }
        public int Member_Id { get; set; }
        public EcommercePayload Payload { get; set; } 
        public DateTime? datetime { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string ItemCount { get; set; }
    }
}