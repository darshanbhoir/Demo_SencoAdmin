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
    
    public partial class tbl_events
    {
        public int event_no { get; set; }
        public string event_name { get; set; }
        public string event_desc { get; set; }
        public string event_banner { get; set; }
        public string event_banner_thumbnail { get; set; }
        public Nullable<bool> is_active { get; set; }
        public string event_url { get; set; }
        public Nullable<int> clicks { get; set; }
    }
}