using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.StaffManagement;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set{ staffObservation = value; OnPropertyChanged(); }
        }

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



        public ICommand OpenAddWindowCommand { get; }
        public ICommand CloseAddWindowCommand { get; }
        public ICommand SearchStaff { get; }
        public ICommand AddStaffCommand { get; }
        public ICommand PasswordChangedCommand { get; }
        public ICommand ConfirmPasswordChangedCommand { get; }
        public StaffViewModel()
        {
            StartDate = DateTime.Now;
            BirthDay = DateTime.Now;
            FirstLoadStaffPage = new RelayCommand<object>( null, async (p) =>
            {
                StaffObservation = new ObservableCollection<StaffDTO>(await Task.Run(()=> StaffService.Ins.GetAllStaff()));
                if(StaffObservation.Count > 0)
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
                int dWage;
                if (DisplayName == null || StartDate == null || UserName == null || PassWord == null || PhoneNumber == null || BirthDay == null || Wage == null || Status == null || Email == null || Gender == null || Role == null || !int.TryParse(Wage, out dWage))
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }
                else
                {
                    Staff newStaff = new Staff
                    {
                        DisplayName = this.DisplayName,
                        StartDate = this.StartDate,
                        UserName = this.UserName,
                        PassWord = this.PassWord,
                        PhoneNumber = this.PhoneNumber,
                        BirthDay = this.BirthDay,
                        Wage = dWage,
                        Status = this.Status,
                        Email = this.Email,
                        Gender = this.Gender,
                        Role = this.Role,
                        IsDeleted = false
                    };
                    (bool IsAdded, string messageAdd) = await StaffService.Ins.AddNewStaff(newStaff);
                    if (IsAdded)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Add, "Bạn đã thêm thành công nhân viên");
                        StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
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
                AddStaff addStaffWindow = new AddStaff();
                addStaffWindow.Owner = Window.GetWindow(mainStaffWindow);
                addStaffWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
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

            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                confirmPassWord = p.Password;
            });


        }
    }
}
