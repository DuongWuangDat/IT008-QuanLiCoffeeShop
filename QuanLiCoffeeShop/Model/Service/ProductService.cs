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
			var productList = (from c in DataProvider.Ins.DB.Product
							   where c.IsDeleted== false
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
		
		//Add new product

		public async Task<(bool, string)> AddNewPrD(Product newPrD)
		{
			var prD = await DataProvider.Ins.DB.Product.Where(p => p.DisplayName == newPrD.DisplayName).FirstOrDefaultAsync();
			if(prD != null)
			{
				if(prD.IsDeleted== true)
				{
					prD.DisplayName = newPrD.DisplayName;
					prD.Price = newPrD.Price;
					prD.IDGenre = newPrD.IDGenre;
					prD.GenreProduct = newPrD.GenreProduct;
					prD.Count = newPrD.Count;
					prD.Description = newPrD.Description;
					prD.Image = newPrD.Image;
					prD.IsDeleted = false;
                    await DataProvider.Ins.DB.SaveChangesAsync();
                    return (true, "Thêm thành công");
                }
				else
				{
                    return (false, "Đã tồn tại sản phẩm");
                }
			}
			DataProvider.Ins.DB.Product.Add(newPrD);
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Thêm thành công");
		}

        //Delete product
        public async Task<(bool, string)> DeletePrD(int ID)
        {
			var prD = await DataProvider.Ins.DB.Product.Where(p => p.ID == ID).FirstOrDefaultAsync();
			if(prD.IsDeleted==false) prD.IsDeleted = true;
            await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Xóa thành công");
        }

		//Edit product
		public async Task<(bool, string)> EditPrD(Product newPrD)
		{
            var prD = await DataProvider.Ins.DB.Product.Where(p => p.ID == newPrD.ID).FirstOrDefaultAsync();
            prD.DisplayName = newPrD.DisplayName;
            prD.Price = newPrD.Price;
            prD.IDGenre = newPrD.IDGenre;
            prD.GenreProduct = newPrD.GenreProduct;
            prD.Count = newPrD.Count;
            prD.Description = newPrD.Description;
            prD.Image = newPrD.Image;
            prD.IsDeleted = false;
			await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Cập nhật thành công");

        }
    }
}
