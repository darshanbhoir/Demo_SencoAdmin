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
    
    public partial class tbl_store_reviews
    {
        public int store_review_no { get; set; }
        public int store_no { get; set; }
        public int user_no { get; set; }
        public Nullable<int> store_rating { get; set; }
        public string user_review { get; set; }
        public Nullable<System.DateTime> post_date { get; set; }
    
        public virtual tbl_store_master tbl_store_master { get; set; }
        public virtual tbl_user_details tbl_user_details { get; set; }
    }
}
