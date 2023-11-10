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
		private QuanLiCoffeShopEntities context;
        private static StaffService _ins;

		public static StaffService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new StaffService();
				}
				if (_ins.context == null)
				{
					_ins.context = new QuanLiCoffeShopEntities();
				}
				return _ins;
			}
			private set { _ins = value; }
		}

		//Get all staff
		public async Task<List<StaffDTO>> GetAllStaff()
		{
				var staffList = (from c in context.Staff
								 where c.IsDeleted == false
								 select new StaffDTO
								 {
									 ID = c.ID,
									 DisplayName = c.DisplayName,
									 StartDate = c.StartDate,
									 UserName = c.UserName,
									 PassWord = c.PassWord,
									 PhoneNumber = c.PhoneNumber,
									 BirthDay = c.BirthDay,
									 Wage = c.Wage,
									 Status = c.Status,
									 Email = c.Email,
									 Gender = c.Gender,
									 Role = c.Role,
									 IsDeleted = c.IsDeleted,
								 }).ToListAsync();
				return await staffList;
			}

		//Add staff
		public async Task<(bool,string)> AddNewStaff(Staff newStaff)
		{
			bool IsEsixtEmail = await context.Staff.AnyAsync(p=> p.Email== newStaff.Email );
			bool IsExistPhone = await context.Staff.AnyAsync(p => p.PhoneNumber == newStaff.PhoneNumber);
			var staff = await context.Staff.Where(p => p.Email == newStaff.Email || p.PhoneNumber == newStaff.PhoneNumber).FirstOrDefaultAsync();
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
					await context.SaveChangesAsync();
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


		// Edit staff
		public async Task<(bool, string)> EditStaff(Staff newStaff)
		{
			var staff = await DataProvider.Ins.DB.Staff.Where(p => p.ID == newStaff.ID).FirstOrDefaultAsync();
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
            await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Cap that thanh cong");
		}

		//Delete staff
		public async Task<(bool, string)>  DeleteStaff(int ID)
		{
            var staff = await DataProvider.Ins.DB.Staff.Where(p => p.ID == ID).FirstOrDefaultAsync();
			if (staff.IsDeleted == false) staff.IsDeleted = true;
            await DataProvider.Ins.DB.SaveChangesAsync();
            return (true, "Da xoa");
		}
	}
}
