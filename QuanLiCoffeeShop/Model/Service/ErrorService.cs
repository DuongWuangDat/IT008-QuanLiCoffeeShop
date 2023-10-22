using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.RightsManagement;
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

		//Get all error
		public async Task<List<ErrorDTO>> GetAllError()
		{
			var errList = (from c in DataProvider.Ins.DB.Error
						   select new ErrorDTO
						   {
							   ID = c.ID,
							   DisplayName = c.DisplayName,
							   Status = c.Status,
								Description= c.Description,
								IsDeleted = c.IsDeleted,
						   }).ToListAsync();
			return await errList;
		}
		
		//Add error
		public async Task<(bool, string)> AddNewError(Error newError)
		{
			DataProvider.Ins.DB.Error.Add(newError);
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Them thanh cong");
		}
		
		//Edit error
		public async Task<(bool,string)> EditError(Error newError)
		{
			var err = await DataProvider.Ins.DB.Error.Where(p=> p.ID == newError.ID).FirstOrDefaultAsync();
			err.Description = newError.Description;
			err.Status = newError.Status;
			err.DisplayName = newError.DisplayName;
			err.IsDeleted = false;
			return (true, "Cap nhat thanh cong");
		}

		//Delete Error
		public async Task<(bool, string)>  DeleteError(int ID)
		{
            var err = await DataProvider.Ins.DB.Error.Where(p => p.ID == ID).FirstOrDefaultAsync();
			if (err.IsDeleted==false) err.IsDeleted=true;
			else
			{
				return (false, "Da xoa");
			}
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Xoa thanh cong");
        }
	}
}
