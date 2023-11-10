using QuanLiCoffeeShop.DTOs;
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
		private static GenreService _ins;

		public static GenreService Ins
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

        //Get  all gerne seat
        public async Task<List<string>> GetAllSeat()
        {
            var seatGenreList = (from c in DataProvider.Ins.DB.GenreSeat select c.DisplayName).ToListAsync();
            return await seatGenreList;
        }

        // Get all genre prD
        public async Task<List<string>> GetAllPrD()
        {
            var productGenreList = (from c in DataProvider.Ins.DB.GenreProduct select c.DisplayName).ToListAsync();
            return await productGenreList;
        }
    }
}
