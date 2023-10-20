using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
	public class CustomerService
	{
		public CustomerService() { }
		private static CustomerService _ins;

		public static CustomerService Ins
		{
			get
			{
				if (_ins == null)
				{
					_ins = new CustomerService();
				}
				return _ins;
			}
			private set { _ins = value; }
		}
		public async Task<List<CustomerDTO>> GetAllCus()
		{
			var cusList = (from s in DataProvider.Ins.DB.Customer
						   where s.IsDeleted == false
						   select new CustomerDTO
						   {
							   ID = s.ID,
							   Description = s.Description,
							   DisplayName = s.DisplayName,
							   Email = s.Email,
							   IDSeat = s.IDSeat,
							   IsDeleted = s.IsDeleted,
							   PhoneNumber = s.PhoneNumber,
							   Spend = s.Spend,
						   }).ToListAsync();
			return await cusList;
		}
		public async Task<(bool, string)> AddNewCus(Customer newCus)
		{
			bool IsEmailExist = await DataProvider.Ins.DB.Customer.AnyAsync(p => p.Email == newCus.Email);
			bool IsPhoneExist = await DataProvider.Ins.DB.Customer.AnyAsync(p => p.PhoneNumber == newCus.PhoneNumber);
			
			var cus = await DataProvider.Ins.DB.Customer.Where(p => p.Email == newCus.Email || p.PhoneNumber == newCus.PhoneNumber).FirstOrDefaultAsync();
			if(cus != null)
			{
				if (cus.IsDeleted == true)
				{
					cus.DisplayName = newCus.DisplayName;
					cus.Spend = newCus.Spend;
					cus.PhoneNumber = newCus.PhoneNumber;
					cus.Email = newCus.Email;
					cus.Description = newCus.Description;
					cus.IsDeleted= false;
                    await DataProvider.Ins.DB.SaveChangesAsync();
                    return (true, "Khoi phuc tai khoan thanh cong");
				}
				else
				{
                    if (IsEmailExist)
                    {
                        return (false, "Đã tồn tại email này");
                    }
                    if (IsPhoneExist)
                    {
                        return (false, "Đã tồn tại số điện thoại này");
                    }
                }
			}
			DataProvider.Ins.DB.Customer.Add(newCus);
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Đăng kí khách hàng thành công");
		}
		public async Task<(bool, string)> EditCusList(Customer newCus, int ID)
		{
			var cus = await DataProvider.Ins.DB.Customer.Where(p => p.ID == ID).FirstOrDefaultAsync();
			if (cus == null) return (false, "Không tìm thấy ID");
			cus.Email= newCus.Email;
			cus.PhoneNumber= newCus.PhoneNumber;
			cus.Spend= newCus.Spend;
			cus.DisplayName = newCus.DisplayName;
			cus.Description= newCus.Description;
			await DataProvider.Ins.DB.SaveChangesAsync();
			return (true, "Chỉnh sửa thành công");
		}
		public async Task<(bool,string)> DeleteCustomer(int ID)
		{
            var cus = await DataProvider.Ins.DB.Customer.Where(p => p.ID == ID).FirstOrDefaultAsync();
			if (cus.IsDeleted== true) return (false, "Đã xóa khách hàng này rồi");
			cus.IsDeleted = true;
            await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Xóa thành công");
		}
	}
}

