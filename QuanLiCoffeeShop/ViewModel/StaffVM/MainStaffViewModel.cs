﻿using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.MessageBox;
using QuanLiCoffeeShop.View.Staff.ReportError;
using QuanLiCoffeeShop.View.Staff.Sales;
using QuanLiCoffeeShop.View.Staff.SalesHistory;
using QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.StaffVM
{
    public class MainStaffViewModel : BaseViewModel
    {
        public static StaffDTO currentStaff;
        private string _currentName;
        public string currentName
        {
            get { return _currentName; }
            set { _currentName = value; OnPropertyChanged(); }
        }

        #region Account Information
        private string editDisplayName;

        public string EditDisplayName
        {
            get { return editDisplayName; }
            set { editDisplayName = value; }
        }

        private string editStartDate;

        public string EditStartDate
        {
            get { return editStartDate; }
            set { editStartDate = value; }
        }

        private string editUserName;

        public string EditUserName
        {
            get { return editUserName; }
            set { editUserName = value; }
        }

        private string editPassWord;

        public string EditPassWord
        {
            get { return editPassWord; }
            set { editPassWord = value; }
        }

        private string editPhoneNumber;

        public string EditPhoneNumber
        {
            get { return editPhoneNumber; }
            set { editPhoneNumber = value; }
        }

        private string editBirthDay;

        public string EditBirthDay
        {
            get { return editBirthDay; }
            set { editBirthDay = value; }
        }

        private string editEmail;
        public string EditEmail
        {
            get { return editEmail; }
            set { editEmail = value; }
        }

        private string editWage;

        public string EditWage
        {
            get { return editWage; }
            set { editWage = value; }
        }

        private string editStatus;

        public string EditStatus
        {
            get { return editStatus; }
            set { editStatus = value; }
        }

        private string editGender;

        public string EditGender
        {
            get { return editGender; }
            set
            {
                editGender = value;
                OnPropertyChanged(nameof(EditGender));
            }
        }

        private string editRole;

        public string EditRole
        {
            get { return editRole; }
            set { editRole = value; }
        }
        #endregion


        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadBanHangPageCM { get; set; }
        public ICommand LoadErrorPageCM { get; set; }
        public ICommand LoadHistoryPageCM { get; set; }
        public ICommand FirstLoadAccountWindow { get; set; }
        public ICommand OpenAccountWindow { get; set; }
        public ICommand EditStaffCommand { get; set; }
        public ICommand OpenHelpPage { get; set; }
        public ICommand LogOutCommand { get; set; }

        public MainStaffViewModel()
        {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new SalesMainPage();
                currentName = currentStaff.DisplayName;
            });
            LoadBanHangPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new SalesMainPage();
                SalesMainPageViewModel.currentStaff = currentStaff;
            });
            LoadErrorPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ReportError();
            });
            LoadHistoryPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new SalesHistory();
            });
            OpenAccountWindow = new RelayCommand<object>(null, (p) =>
            {
                EditBirthDay = currentStaff.BirthDay.ToString();
                EditDisplayName = currentStaff.DisplayName;
                EditEmail = currentStaff.Email;
                EditGender = currentStaff.Gender.Trim();
                EditPassWord = null;
                EditPhoneNumber = currentStaff.PhoneNumber;
                EditRole = currentStaff.Role;
                EditStartDate = currentStaff.StartDate.ToString();
                EditStatus = currentStaff.Status;
                EditUserName = currentStaff.UserName;
                EditWage = ((int)currentStaff.Wage).ToString("N0");
                View.Staff.AccountInformation account = new View.Staff.AccountInformation();
                account.ShowDialog();
            });
            EditStaffCommand = new RelayCommand<Window>(null, async (p) =>
            {
                if (EditDisplayName == null || EditUserName == null || EditPhoneNumber == null || EditEmail == null
                || EditDisplayName == "" || EditUserName == "" || EditPhoneNumber == "" || EditEmail == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }

                else
                {
                    if ((EditPassWord == null || EditPassWord == "") && EditDisplayName == currentStaff.DisplayName && EditEmail == currentStaff.Email && EditUserName == currentStaff.UserName && EditPhoneNumber == currentStaff.PhoneNumber)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Không có gì mới để chỉnh sửa");
                        p.Close();
                        return;
                    }

                    string pass;
                    if (EditPassWord == null || EditPassWord == "")
                    {
                        EditPassWord = currentStaff.PassWord;
                        pass = EditPassWord;
                    }
                    else
                        pass = Helper.MD5Hash(EditPassWord);
                    Staff newStaff = new Staff
                    {
                        ID = currentStaff.ID,
                        DisplayName = this.EditDisplayName,
                        Email = this.EditEmail,
                        Gender = this.EditGender,
                        StartDate = currentStaff.StartDate,
                        Status = this.EditStatus,
                        UserName = this.EditUserName,
                        PassWord = pass,
                        BirthDay = currentStaff.BirthDay,
                        PhoneNumber = this.EditPhoneNumber,
                        Role = this.EditRole,
                        Wage = currentStaff.Wage,
                        IsDeleted = false
                    };
                    (bool success, string messageEdit) = await StaffService.Ins.EditStaff(newStaff);
                    if (success)
                    {
                        using (var context = new QuanLiCoffeShopEntities())
                        {
                            Staff staff = await context.Staff.Where(x => x.ID == currentStaff.ID).FirstOrDefaultAsync();
                            StaffDTO curStaff = new StaffDTO
                            {
                                ID = staff.ID,
                                DisplayName = staff.DisplayName,
                                StartDate = staff.StartDate,
                                UserName = staff.UserName,
                                PassWord = staff.PassWord,
                                PhoneNumber = staff.PhoneNumber,
                                BirthDay = staff.BirthDay,
                                Wage = staff.Wage,
                                Status = staff.Status,
                                Email = staff.Email,
                                Gender = staff.Gender,
                                Role = staff.Role,
                                IsDeleted = staff.IsDeleted,
                            };
                            currentStaff = curStaff;
                            currentName = currentStaff.DisplayName;
                        }
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã chỉnh sửa thành công");
                        p.Close();
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                    }
                }
            });
            OpenHelpPage = new RelayCommand<object>(null, (p) =>
            {
                try
                {
                    Process.Start("https://www.facebook.com/jack.phuongtuan1204");
                }
                catch (Exception)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi mở trang web");
                }
            });
            LogOutCommand = new RelayCommand<Window>(null, (p) =>
            {
                ConfirmLogOut confirmLogOut = new ConfirmLogOut();
                confirmLogOut.ShowDialog();
                if (confirmLogOut.DialogResult == true)
                {
                    p.Close();
                }
            });
        }
    }
}
