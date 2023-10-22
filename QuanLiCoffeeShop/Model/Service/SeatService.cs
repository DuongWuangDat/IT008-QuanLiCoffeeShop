using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class SeatService
    {
        public SeatService() { }
		private static SeatService _ins;

		public static SeatService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new SeatService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}

		//Get all seat
		
	}
}
