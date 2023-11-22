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
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.BillInfo = new HashSet<BillInfo>();
        }
    
        public int ID { get; set; }
        public Nullable<int> IDCus { get; set; }
        public Nullable<int> IDStaff { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> IDSeat { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual Staff Staff { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillInfo> BillInfo { get; set; }
    }
}
