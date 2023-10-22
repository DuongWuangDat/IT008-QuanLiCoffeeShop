using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class GenreService
    {
		private GenreService _ins;

		public GenreService Ins
		{
			get 
			{ 
				if(_ins == null) _ins = new GenreService();
				return _ins; 
			}
			private set { _ins = value; }
		}
        public async Task<(int, GenreProduct)> FindGenrePrD(string name)
        {
            var prD = await DataProvider.Ins.DB.GenreProduct.Where(p => p.DisplayName == name).FirstOrDefaultAsync();
            if (prD != null)
            {
                return (-1, null);
            }
            return (prD.ID, prD);
        }
    }
}
