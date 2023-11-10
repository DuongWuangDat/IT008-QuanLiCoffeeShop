using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
        //Get all product
        public async Task<List<ProductDTO>> GetAllProduct()
		{
			using(var context = new QuanLiCoffeShopEntities())
			{
                var productList = (from c in context.Product
                                   where c.IsDeleted == false
                                   select new ProductDTO
                                   {
                                       ID = c.ID,
                                       DisplayName = c.DisplayName,
                                       Price = c.Price,
                                       IDGenre = c.IDGenre,
                                       GenreName = c.GenreProduct.DisplayName,
                                       Count = c.Count,
                                       Description = c.Description,
                                       Image = c.Image,
                                       IsDeleted = c.IsDeleted,
                                   }).ToListAsync();
                return await productList;
            }
			
		}
		
		//Add new product

		public async Task<(bool, string)> AddNewPrD(Product newPrD)
		{
			using(var context = new QuanLiCoffeShopEntities())
			{
                var prD = await context.Product.Where(p => p.DisplayName == newPrD.DisplayName).FirstOrDefaultAsync();
                if (prD != null)
                {
                    if (prD.IsDeleted == true)
                    {
                        prD.DisplayName = newPrD.DisplayName;
                        prD.Price = newPrD.Price;
                        prD.IDGenre = newPrD.IDGenre;
                        prD.GenreProduct = newPrD.GenreProduct;
                        prD.Count = newPrD.Count;
                        prD.Description = newPrD.Description;
                        prD.Image = newPrD.Image;
                        prD.IsDeleted = false;
                        await context.SaveChangesAsync();
                        return (true, "Them thanh cong");
                    }
                    else
                    {
                        return (false, "Da ton tai san pham");
                    }
                }
                context.Product.Add(newPrD);
                await context.SaveChangesAsync();
                return (true, "Them thanh cong");
            }
			
		}

        //Delete product
        public async Task<(bool, string)> DeletePrD(int ID)
        {
            using(var context = new QuanLiCoffeShopEntities())
            {
                var prD = await context.Product.Where(p => p.ID == ID).FirstOrDefaultAsync();
                if (prD.IsDeleted == false) prD.IsDeleted = true;
                await context.SaveChangesAsync();
                return (true, "Xoa thanh cong");
            }
			
        }

		//Edit product
		public async Task<(bool, string)> EditPrD(Product newPrD)
		{
            using(var context = new QuanLiCoffeShopEntities())
            {
                var prD = await context.Product.Where(p => p.ID == newPrD.ID).FirstOrDefaultAsync();
                prD.DisplayName = newPrD.DisplayName;
                prD.Price = newPrD.Price;
                prD.IDGenre = newPrD.IDGenre;
                prD.GenreProduct = newPrD.GenreProduct;
                prD.Count = newPrD.Count;
                prD.Description = newPrD.Description;
                prD.Image = newPrD.Image;
                prD.IsDeleted = false;
                await context.SaveChangesAsync();
                return (true, "Cap nhat thanh cong");
            }
            

        }
    }
}
