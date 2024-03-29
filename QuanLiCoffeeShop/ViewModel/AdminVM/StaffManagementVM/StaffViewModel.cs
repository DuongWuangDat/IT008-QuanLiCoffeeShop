﻿using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel
{


    public class StaffViewModel : BaseViewModel
    {

        private List<StaffDTO> staffList; //Instance of database
        private ObservableCollection<StaffDTO> staffObservation; //ListView source
        public ObservableCollection<StaffDTO> StaffObservation
        {
            get { return staffObservation; }
            set
            {
                staffObservation = value; OnPropertyChanged(nameof(StaffObservation));
                OnPropertyChanged(nameof(Count));
            }
        }
        public int Count => StaffObservation?.Count ?? 0;

        public ICommand FirstLoadStaffPage { get; }

        //Add Staff
        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private string confirmPassWord;

        public string ConfirmPassWord
        {
            get { return confirmPassWord; }
            set { confirmPassWord = value; }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private DateTime birthDay;
        public DateTime BirthDay
        {
            get { return birthDay; }
            set { birthDay = value; }
        }

        private string wage;

        public string Wage
        {
            get { return wage; }
            set { wage = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string role;

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        private StaffDTO selectedItem;

        public StaffDTO SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        // Edit Staff
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
                if (GenderList.Contains(value))
                {
                    editGender = value;
                    OnPropertyChanged(nameof(EditGender));
                }
            }
        }

        private string editRole;

        public string EditRole
        {
            get { return editRole; }
            set { editRole = value; }
        }

        private ObservableCollection<string> _genderList;

        public ObservableCollection<string> GenderList
        {
            get { return _genderList; }
            set
            {
                _genderList = value;
                OnPropertyChanged(nameof(GenderList));
            }
        }

        private ObservableCollection<string> _statusList;

        public ObservableCollection<string> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged(nameof(StatusList));
            }
        }

        private ObservableCollection<string> _roleList;

        public ObservableCollection<string> RoleList
        {
            get { return _roleList; }
            set { _roleList = value; }
        }


        public ICommand OpenAddWindowCommand { get; }
        public ICommand CloseAddWindowCommand { get; }
        public ICommand SearchStaff { get; }
        public ICommand AddStaffCommand { get; }
        public ICommand PasswordChangedCommand { get; }
        public ICommand ConfirmPasswordChangedCommand { get; }
        public ICommand DeleteStaffCommand { get; }
        public ICommand EditStaffCommand { get; }
        public ICommand OpenEditStaffCommand { get; }
        public ICommand CloseEditStaffCommand { get; }
        public ICommand EditPasswordChangedCommand { get; }
        
        public StaffViewModel()
        {
            StartDate = DateTime.Now;
            BirthDay = DateTime.Now;
            GenderList = new ObservableCollection<string> { "Nam", "Nữ" };
            StatusList = new ObservableCollection<string> { "Đang làm", "Xin nghỉ" };
            RoleList = new ObservableCollection<string> { "Quản lí", "Nhân viên" };
            FirstLoadStaffPage = new RelayCommand<object>(null, async (p) =>
            {
                StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                if (StaffObservation.Count > 0)
                    staffList = new List<StaffDTO>(StaffObservation);
            });

            SearchStaff = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (staffList != null)
                        StaffObservation = new ObservableCollection<StaffDTO>(staffList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }
            });

            AddStaffCommand = new RelayCommand<Window>(null, async (p) =>
            {
                int iWage = 0;
                if (DisplayName == null || UserName == null || PassWord == null
                || PhoneNumber == null || Wage == null || Status == null || Email == null
                || Gender == null || Role == null || !int.TryParse(Wage.Replace(".", ""), out iWage)
                || DisplayName == "" || UserName == "" || PassWord == ""
                || PhoneNumber == "" || Wage == "" || Status == "" || Email == ""
                || Gender == "" || Role == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }

                
                else
                {
                    if (PassWord != ConfirmPassWord)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xác nhận mật khẩu không đúng");
                        return;
                    }
                    if (DateTime.Compare(BirthDay, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(BirthDay, DateTime.Now) > 0)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày sinh không hợp lệ");
                        return;
                    }    

                    else if (DateTime.Compare(StartDate, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(StartDate, DateTime.Now) > 0)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày bắt đầu không hợp lệ");
                        return;
                    }    

                    else if (StartDate.Year - (BirthDay).Year < 16)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Đảm bảo nhân viên vào làm trên 16 tuổi");
                        return;
                    }    
                    string pass = Helper.MD5Hash(this.PassWord);

                    Staff newStaff = new Staff
                    {
                        DisplayName = this.DisplayName,
                        StartDate = this.StartDate,
                        UserName = this.UserName,
                        PassWord = pass,
                        PhoneNumber = this.PhoneNumber,
                        BirthDay = this.BirthDay,
                        Wage = iWage,
                        Status = this.Status,
                        Email = this.Email,
                        Gender = this.Gender,
                        Role = this.Role,
                        IsDeleted = false
                    };
                    (bool IsAdded, string messageAdd) = await StaffService.Ins.AddNewStaff(newStaff);
                    if (IsAdded)
                    {
                        StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã thêm thành công nhân viên");
                        p.Close();
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageAdd);
                    }
                }

            });

            OpenAddWindowCommand = new RelayCommand<Page>((mainStaffWindow) => { return true; }, (mainStaffWindow) =>
            {
                DisplayName = null;
                StartDate = DateTime.Now;
                UserName = null;
                PassWord = null;
                PhoneNumber = null;
                BirthDay = DateTime.Now;
                Wage = null;
                Status = null;
                Email = null;
                Gender = null;
                Role = null;
                ConfirmPassWord = null;
                AddStaff addStaffWindow = new AddStaff();
                addStaffWindow.ShowDialog();
            });

            CloseAddWindowCommand = new RelayCommand<Window>((mainStaffWindow) => { return true; }, (mainStaffWindow) =>
            {
                mainStaffWindow.Close();
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                passWord = p.Password;
            });

			EditPasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
			{
				EditPassWord = p.Password;
			});

			ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                confirmPassWord = p.Password;
            });

            OpenEditStaffCommand = new RelayCommand<object>(null, (p) =>
            {
                EditBirthDay = SelectedItem.BirthDay.ToString();
                EditDisplayName = SelectedItem.DisplayName;
                EditEmail = SelectedItem.Email;
                EditGender = SelectedItem.Gender.Trim();
                EditPassWord = null;
                EditPhoneNumber = SelectedItem.PhoneNumber;
                EditRole = SelectedItem.Role;
                EditStartDate = SelectedItem.StartDate.ToString();
                EditStatus = SelectedItem.Status;
                EditUserName = SelectedItem.UserName;
                EditWage = ((int)SelectedItem.Wage).ToString();
                ModifyStaff modifyStaff = new ModifyStaff();
                modifyStaff.ShowDialog();
            });

            EditStaffCommand = new RelayCommand<Window>(null, async (p) =>
            {
                int iWage;
                if (EditDisplayName == null || EditStartDate == null || EditUserName == null || EditPhoneNumber == null
                || EditBirthDay == null || EditWage == null || EditStatus == null || EditEmail == null || EditGender == null
                || EditRole == null || !int.TryParse(EditWage.Replace(".", ""), out iWage)
                || EditDisplayName == "" || EditUserName == "" || EditPhoneNumber == ""
                || EditWage == "" || EditStatus == "" || EditEmail == "" || EditGender == ""
                || EditRole == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }

                else
                {
                    DateTime tempBirthDay;
                    DateTime.TryParseExact(EditBirthDay, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out tempBirthDay);

                    DateTime tempStartDate;
                    DateTime.TryParseExact(EditStartDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out tempStartDate);

                    if ((EditPassWord == null || EditPassWord == "") && EditDisplayName == SelectedItem.DisplayName && EditEmail == SelectedItem.Email
                        && EditGender == SelectedItem.Gender.Trim() && tempStartDate == SelectedItem.StartDate && EditStatus == SelectedItem.Status
                        && EditUserName == SelectedItem.UserName && tempBirthDay == SelectedItem.BirthDay && EditPhoneNumber == SelectedItem.PhoneNumber
                        && EditRole == SelectedItem.Role && iWage == SelectedItem.Wage)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Không có gì mới để chỉnh sửa");
                        p.Close();
                        return;
                    }

                    if (DateTime.Compare(tempBirthDay, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(tempBirthDay, DateTime.Now) > 0)
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày sinh không hợp lệ");

                    else if (DateTime.Compare(tempStartDate, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(tempStartDate, DateTime.Now) > 0)
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày bắt đầu không hợp lệ");

                    else if (tempStartDate.Year - tempBirthDay.Year < 16)
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Đảm bảo nhân viên vào làm trên 16 tuổi");

                    else
                    {
                        string pass;
                        if (EditPassWord == null || EditPassWord == "")
                        {
                            EditPassWord = SelectedItem.PassWord;
                            pass = EditPassWord;
                        }
                        else
                            pass = Helper.MD5Hash(EditPassWord);
                        Staff newStaff = new Staff
                        {
                            ID = SelectedItem.ID,
                            DisplayName = this.EditDisplayName,
                            Email = this.EditEmail,
                            Gender = this.EditGender,
                            StartDate = tempStartDate,
                            Status = this.EditStatus,
                            UserName = this.EditUserName,
                            PassWord = pass,
                            BirthDay = tempBirthDay,
                            PhoneNumber = this.EditPhoneNumber,
                            Role = this.EditRole,
                            Wage = iWage,
                            IsDeleted = false
                        };
                        (bool success, string messageEdit) = await StaffService.Ins.EditStaff(newStaff);
                        if (success)
                        {
                            StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã chỉnh sửa thành công");
                            p.Close();
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                        }
                    }

                }


            });

            CloseEditStaffCommand = new RelayCommand<Window>(null, (p) =>
            {
                p.Close();
            });

            DeleteStaffCommand = new RelayCommand<object>(null, async (p) =>
            {
                DeleteMessage deleteMessage = new DeleteMessage();
                deleteMessage.ShowDialog();
                if (deleteMessage.DialogResult == true)
                {
                    (bool IsDeleted, string messageDelete) = await StaffService.Ins.DeleteStaff(selectedItem.ID);
                    StaffObservation.Remove(selectedItem);
                    if (IsDeleted)
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã xóa thành công nhân viên");
                    else
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageDelete);
                }
            });
            

        }
    }
}
