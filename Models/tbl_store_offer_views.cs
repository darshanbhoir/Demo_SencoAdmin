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
    
    public partial class tbl_store_offer_views
    {
        public int stv_no { get; set; }
        public int store_offer_no { get; set; }
        public int user_no { get; set; }
        public Nullable<System.DateTime> viewed_on_datetime { get; set; }
    
        public virtual tbl_store_offers tbl_store_offers { get; set; }
        public virtual tbl_user_details tbl_user_details { get; set; }
    }
}
