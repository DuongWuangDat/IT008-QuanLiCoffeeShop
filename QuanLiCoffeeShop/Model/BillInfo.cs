//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiCoffeeShop.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class BillInfo
    {
        public int IDBill { get; set; }
        public int IDProduct { get; set; }
        public Nullable<decimal> PriceItem { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
