using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> IDGenre { get; set; }
        public Nullable<int> Count { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
