//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo_Senco_Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_astrologer_schedule
    {
        public int as_id { get; set; }
        public Nullable<int> astrologer_id { get; set; }
        public string duration { get; set; }
        public string sunday { get; set; }
        public string monday { get; set; }
        public string tuesday { get; set; }
        public string wednesday { get; set; }
        public string thursday { get; set; }
        public string friday { get; set; }
        public string saturday { get; set; }
        public bool sun { get; set; }
        public bool mon { get; set; }
        public bool tue { get; set; }
        public bool wed { get; set; }
        public bool thu { get; set; }
        public bool fri { get; set; }
        public bool sat { get; set; }
        public string sunday_from { get; set; }
        public string monday_from { get; set; }
        public string tuesday_from { get; set; }
        public string wednesday_from { get; set; }
        public string thursday_from { get; set; }
        public string friday_from { get; set; }
        public string saturday_from { get; set; }
        public string sunday_to { get; set; }
        public string monday_to { get; set; }
        public string tuesday_to { get; set; }
        public string wednesday_to { get; set; }
        public string thursday_to { get; set; }
        public string friday_to { get; set; }
        public string saturday_to { get; set; }
    
        public virtual tbl_astrologer_master tbl_astrologer_master { get; set; }
    }
}
