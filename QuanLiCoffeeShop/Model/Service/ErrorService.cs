using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class ErrorService
    {
        public ErrorService() { }
		private static ErrorService _ins;

		public static ErrorService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new ErrorService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}

	}
}
