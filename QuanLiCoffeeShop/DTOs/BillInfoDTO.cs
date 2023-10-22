using QuanLiCoffeeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class BillInfoDTO
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
