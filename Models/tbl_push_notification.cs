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
    
    public partial class tbl_push_notification
    {
        public int notification_id { get; set; }
        public string notification_image { get; set; }
        public string notification_title { get; set; }
        public string notification_message { get; set; }
        public Nullable<System.DateTime> notification_broadcast { get; set; }
        public string notification_category { get; set; }
        public Nullable<bool> is_active { get; set; }
    }
}
