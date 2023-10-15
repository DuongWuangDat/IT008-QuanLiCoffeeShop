using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class ProductService
    {
        public ProductService() { }
		private static ProductService _ins;

		public static ProductService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new ProductService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}

	}
}
