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
    
    public partial class tbl_designer_appointment
    {
        public int pa_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public string customer_phoneno { get; set; }
        public Nullable<int> state_id { get; set; }
        public Nullable<int> store_id { get; set; }
        public Nullable<System.DateTime> appointment_date { get; set; }
        public string appointment_time { get; set; }
        public string appointment_comments { get; set; }
        public string appointment_no { get; set; }
    
        public virtual tbl_state_master tbl_state_master { get; set; }
        public virtual tbl_store_master tbl_store_master { get; set; }
        public virtual tbl_user_details tbl_user_details { get; set; }
    }
}
