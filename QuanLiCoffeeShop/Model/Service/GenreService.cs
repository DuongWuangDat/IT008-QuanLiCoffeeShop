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
            using (var context = new QuanLiCoffeShopEntities())
            {
                var prD = await context.GenreProduct.Where(p => p.DisplayName == name).FirstOrDefaultAsync();
                if (prD != null)
                {
                    return (-1, null);
                }
                return (prD.ID, prD);
            }
                
        }

        //Get  all gerne seat
        public async Task<List<string>> GetAllSeat()
        {
            using(var context = new QuanLiCoffeShopEntities())
            {
                var seatGenreList = (from c in context.GenreSeat select c.DisplayName).ToListAsync();
                return await seatGenreList;
            }
            
        }

        // Get all genre prD
        public async Task<List<string>> GetAllPrD()
        {
            using(var context = new QuanLiCoffeShopEntities())
            {
                var seatGenreList = (from c in context.GenreProduct select c.DisplayName).ToListAsync();
                return await seatGenreList;
            }
            
        }
    }
}
