using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class BillService
    {
        public BillService() { }
		private static BillService ins;

		public static BillService _ins
		{
			get 
			{
				if (_ins == null)
				{
					_ins = new BillService();
				}
				return ins; 
			}
			private set { ins = value; }
		}

	}
}
