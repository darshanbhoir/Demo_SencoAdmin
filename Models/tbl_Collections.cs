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
    
    public partial class tbl_Collections
    {
        public int Collection_id { get; set; }
        public string Collection_Title { get; set; }
        public string Collection_Image { get; set; }
        public string Collection_url { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<System.DateTime> updated_on { get; set; }
        public Nullable<int> clicks { get; set; }
    }
}
