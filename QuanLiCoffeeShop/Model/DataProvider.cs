﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model
{
    class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins
        {
            get { if (_ins == null) _ins = new DataProvider(); return _ins; }
            set
            {
                _ins = value;
            }
        }
        public QuanLiCoffeShopEntities DB { get; set; }
        private DataProvider()
        {
            try
            {
                DB = new QuanLiCoffeShopEntities();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}