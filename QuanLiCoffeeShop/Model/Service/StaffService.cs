using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
		public async Task<List<StaffDTO>> GetAllStaff()
		{
			var staffList = (from c in DataProvider.Ins.DB.Staff
						   select new StaffDTO
						   {
							   ID = c.ID,
							   DisplayName= c.DisplayName,
							   StartDate = c.StartDate,
							   UserName= c.UserName,
							   PassWord= c.PassWord,
							   PhoneNumber= c.PhoneNumber,
							   BirthDay= c.BirthDay,
							   Wage= c.Wage,
							   Status= c.Status,
							   Email= c.Email,
							   Gender= c.Gender,
							   Role= c.Role,
							   IsDeleted= c.IsDeleted,
						   }).ToListAsync();
			return await staffList;
		}
		public async Task<(bool,string)> AddNewStaff(Staff newStaff)
		{
		
			bool IsEsixtEmail = await DataProvider.Ins.DB.Staff.AnyAsync(p=> p.Email== newStaff.Email );
			bool IsExistPhone = await DataProvider.Ins.DB.Staff.AnyAsync(p => p.PhoneNumber == newStaff.PhoneNumber);
			var staff = await DataProvider.Ins.DB.Staff.Where(p => p.Email == newStaff.Email || p.PhoneNumber == newStaff.PhoneNumber).FirstOrDefaultAsync();
			if (staff != null)
			{
				if (staff.IsDeleted == true)
				{
					staff.DisplayName = newStaff.DisplayName;
					staff.StartDate = newStaff.StartDate;
					staff.UserName = newStaff.UserName;
					staff.PassWord = newStaff.PassWord;
					staff.PhoneNumber = newStaff.PhoneNumber;
					staff.BirthDay = newStaff.BirthDay;
					staff.Wage = newStaff.Wage;
					staff.Status = newStaff.Status;
					staff.Email = newStaff.Email;
					staff.Gender = newStaff.Gender;
					staff.Role = newStaff.Role;
					staff.IsDeleted = false;
					await DataProvider.Ins.DB.SaveChangesAsync();
					return (true, "Them thanh cong");
                }
				else
				{
					if (IsEsixtEmail)
					{
						return (false, "Email da ton tai");
					}
					if (IsExistPhone)
					{
						return (false, "SDT da ton tai");
					}
				}
			}
			DataProvider.Ins.DB.Staff.Add(newStaff);
            await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Them thanh cong");
		}
		
	}
}
