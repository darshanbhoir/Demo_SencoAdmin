using Demo_Senco_Admin.Models.Payload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Demo_Senco_Admin.Models.ViewModel
{
    public class SchemeDashboardViewModel
    {
        [Display(Name = "User No.")]
        public int UserNumber { get; set; }
        public string UserName { get; set; }
        public string UserMobileNumber { get; set; }
        public string UserEmail { get; set; }
        public string CreatedOn { get; set; }
        public string SchemeAccountName { get; set; }
        public string SchemeAccountMobile { get; set; }
        public string SchemeAccountEmail { get; set;}
        public int SchemeRegId { get; set; }
        public string EMIAmount { get; set; }
        public string Location { get; set;}
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int UserCountry { get; set; }
        public int UserCity { get; set; }
        public int? SchemeMemberId { get; set; }
        public DateTime Created_On { get; set; }
        public NewScheme SchemePayload { get; set; }

        
    }
}