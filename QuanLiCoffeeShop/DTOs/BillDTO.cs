﻿using QuanLiCoffeeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class BillDTO
    {
        public int ID { get; set; }
        public Nullable<int> IDCus { get; set; }
        public Nullable<int> IDStaff { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
