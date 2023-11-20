﻿using QuanLiCoffeeShop.DTOs;
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
using QuanLiCoffeeShop.Utils;

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
		}// test

		//Get all staff
		public async Task<List<StaffDTO>> GetAllStaff()
		{
			using(var context = new QuanLiCoffeShopEntities())
			{
                var staffList = await (from c in context.Staff
                                 where c.IsDeleted == false || c.IsDeleted == null
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
                return staffList;
            }
        }
			

		//Add staff
		public async Task<(bool,string)> AddNewStaff(Staff newStaff)
		{
			using(var context = new QuanLiCoffeShopEntities())
			{
                bool IsEsixtEmail = await context.Staff.AnyAsync(p => p.Email == newStaff.Email);
                bool IsExistPhone = await context.Staff.AnyAsync(p => p.PhoneNumber == newStaff.PhoneNumber);
                bool IsExistUsername = await context.Staff.AnyAsync(p => p.UserName == newStaff.UserName);
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
                            return (false, "Email đã tồn tại");
                        }
                        if (IsExistPhone)
                        {
                            return (false, "Số điện thoại đã tồn tại");
                        }
                        if (IsExistUsername)
                        {
                            return (false, "Tài khoản đã tồn tại");
                        }
                    }
                }
                context.Staff.Add(newStaff);
                await context.SaveChangesAsync();
                return (true, "Them thanh cong");
            }
			
		}


		// Edit staff
		public async Task<(bool, string)> EditStaff(Staff newStaff)
		{
            using(var context = new QuanLiCoffeShopEntities())
            {
                var staff = await context.Staff.Where(p => p.ID == newStaff.ID).FirstOrDefaultAsync();
                staff.DisplayName = newStaff.DisplayName;
                staff.StartDate = newStaff.StartDate;
                staff.UserName = newStaff.UserName;
                staff.PassWord =  newStaff.PassWord == null ? staff.PassWord : newStaff.PassWord;
                staff.PhoneNumber = newStaff.PhoneNumber;
                staff.BirthDay = newStaff.BirthDay;
                staff.Wage = newStaff.Wage;
                staff.Status = newStaff.Status;
                staff.Email = newStaff.Email;
                staff.Gender = newStaff.Gender;
                staff.Role = newStaff.Role;
                await context.SaveChangesAsync();
                return (true, "Cap that thanh cong");
            }
			
		}

		//Delete staff
		public async Task<(bool, string)>  DeleteStaff(int ID)
		{
            using(var context = new QuanLiCoffeShopEntities())
            {
                var staff = await context.Staff.Where(p => p.ID == ID).FirstOrDefaultAsync();
                if (staff.IsDeleted == false) staff.IsDeleted = true;
                await context.SaveChangesAsync();
                return (true, "Da xoa");
            }
           
		}
        //update mk
        public async Task<(bool, string,string)> UpdatePassword(string email, string newPass)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var staff = await context.Staff.Where(p => p.Email == email).FirstOrDefaultAsync();
                    if (staff != null && staff.IsDeleted == false)
                    {
                        staff.PassWord = Helper.MD5Hash(newPass);
                    }
                    else
                    {
                        return (false, "Không tồn tại email này", null);
                    }
                    await context.SaveChangesAsync();
                    return (true, "Update mật khẩu thành công", staff.UserName);
                }
            }
            catch (Exception ex)
            {
                return (false, "Có lỗi xuất hiện", null);
            }
        }
	}
}
