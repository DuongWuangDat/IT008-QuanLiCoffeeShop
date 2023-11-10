using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.Problem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.ProblemVM
{
    internal class ProblemViewModel : BaseViewModel
    {
        public static List<ErrorDTO> ProList;
        private ObservableCollection<ErrorDTO> _problemList;

        public ObservableCollection<ErrorDTO> ProblemList
        {
            get { return _problemList; }
            set { _problemList = value; OnPropertyChanged(); }
        }
        private ErrorDTO _selectedItem;

        public ErrorDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private string _name;
        public string Name
        { get { return _name; } set { _name = value; } }

        private string _status;
        public string Status
        { get { return _status; } set { _status = value; } }

        private string _description;
        public string Description
        { get { return _description; } set { _description = value; } }
        private bool isPopupOpenEdit = false;
        public bool IsPopupOpenEdit
        {
            get { return isPopupOpenEdit; }
            set
            {
                isPopupOpenEdit = value;
                OnPropertyChanged(nameof(IsPopupOpenEdit)); // Đảm bảo cập nhật giao diện người dùng khi giá trị này thay đổi.
            }
        }
        private bool isPopupOpenDelete = false;
        public bool IsPopupOpenDelete
        {
            get { return isPopupOpenDelete; }
            set
            {
                isPopupOpenDelete = value;
                OnPropertyChanged(nameof(IsPopupOpenDelete)); // Đảm bảo cập nhật giao diện người dùng khi giá trị này thay đổi.
            }
        }
        private bool isPopupOpenAdd = false;
        private bool isOpenMain = true;
        public bool IsOpenMain { get { return isOpenMain; } set { isOpenMain = value; OnPropertyChanged(nameof(IsOpenMain)); } }
        public bool IsPopupOpenAdd
        {
            get { return isPopupOpenAdd; }
            set
            {
                isPopupOpenAdd = value;
                IsOpenMain = !value;
                OnPropertyChanged(nameof(IsPopupOpenAdd)); // Đảm bảo cập nhật giao diện người dùng khi giá trị này thay đổi.

            }
        }

        public ICommand FirstLoadProblem { get; set; }
        public ICommand OpenAdd { get; set; }
        public ICommand CloseAdd { get; set; }
        public ICommand AddProblem { get; set; }
        public ProblemViewModel()
        {
            FirstLoadProblem = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                ProList = new List<ErrorDTO>(ProblemList);
            });
            OpenAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenAdd = true;
            });
            CloseAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenAdd = false;
            });
            AddProblem = new RelayCommand<object>((p) => { return true; },async (p) =>
            {
                if (this.Name == null || this.Status == null)
                {
                    IsPopupOpenAdd = false;
                    MessageBox.Show("Bạn chưa nhập đủ thông tin!!");
                    IsPopupOpenAdd = true;
                }
                else
                {
                    if (this.Description == null) { this.Description = ""; }
                    Error newerror = new Error
                    {
                        DisplayName = this.Name,
                        Status = this.Status,
                        Description = this.Description,
                        IsDeleted = false,
                    };
                    (bool IsAdded, string messageAdd) = await ErrorService.Ins.AddNewError(newerror);
                    if (IsAdded)
                    {
                        ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                        IsPopupOpenAdd = false;
                    }
                    else
                    {
                        IsPopupOpenAdd= false;
                        MessageBox.Show(messageAdd);




                    }
                }

            });


        }
    }
}
