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
    
    public partial class tbl_astrologer_master
    {
        public tbl_astrologer_master()
        {
            this.tbl_astrologer_appointment = new HashSet<tbl_astrologer_appointment>();
            this.tbl_astrologer_schedule = new HashSet<tbl_astrologer_schedule>();
            this.tbl_astrologer_store_link = new HashSet<tbl_astrologer_store_link>();
        }
    
        public int astrolger_id { get; set; }
        public string astrologer_name { get; set; }
        public string astrologer_mobile_no { get; set; }
        public string astrologer_email_address { get; set; }
        public string astrologer_photo { get; set; }
        public Nullable<bool> status { get; set; }
        public string astrologer_profile { get; set; }
        public Nullable<decimal> astrologer_price { get; set; }
        public Nullable<bool> available_online { get; set; }
        public Nullable<int> astrologer_sequence { get; set; }
    
        public virtual ICollection<tbl_astrologer_appointment> tbl_astrologer_appointment { get; set; }
        public virtual ICollection<tbl_astrologer_schedule> tbl_astrologer_schedule { get; set; }
        public virtual ICollection<tbl_astrologer_store_link> tbl_astrologer_store_link { get; set; }
    }
}