//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterPiece_UsingMVCBackEnd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product_list
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> price { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        public Nullable<int> category_id { get; set; }
        public Nullable<int> sales_count { get; set; }
        public Nullable<int> popularity { get; set; }
        public string date_added { get; set; }
    }
}
