using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class StaffService
    {
        public StaffService() { }
		private static StaffService _ins;

		public static StaffService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new StaffService();
				}
				return _ins;
			}
			private set { _ins = value; }
		}

	}
}
