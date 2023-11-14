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
using QuanLiCoffeeShop.View.Admin.Problem.Problem_page_main;

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
        { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private string _status;
        public string Status
        { get { return _status; } set { _status = value; OnPropertyChanged(); } }

        private string _description;
        public string Description
        { get { return _description; } set { _description = value; OnPropertyChanged(); } }
        private bool isPopupOpenEdit = false;
        public bool IsPopupOpenEdit
        {
            get { return isPopupOpenEdit; }
            set
            {
                isPopupOpenEdit = value;
                IsOpenMain = !value;
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
                IsOpenMain = !value;
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
        public ICommand Edit { get; set; }
        public ICommand OpenEdit { get; set; }
        public ICommand CloseEdit { get; set; }
        public ICommand OpenDelete { get; set; }
        public ICommand CloseDelete { get; set; }
        public ICommand Delete {  get; set; }
        public ICommand Search { get; set; }
        public ICommand CloseMessAdd { get; set; }
        public ICommand CloseMessCauAdd { get; set; }   
        public ICommand CloseMessEdit { get; set; }
        public ICommand CloseMessDelete { get; set; }   
        private void resetdata()
        {
            Name = null; Description=null;Status = null;
        }
        private void ExecuteOpenEdit(ErrorDTO Item)
        {
            IsPopupOpenEdit = true;
            SelectedItem = null;
          SelectedItem = Item;
        }
        private void ExecuteOpenDelete(ErrorDTO Item)
        {          
            SelectedItem = null;
            SelectedItem = Item;
        }

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
                resetdata();
            });
            AddProblem = new RelayCommand<object>((p) => { return true; },async (p) =>
            {
                if (Name == null || Status == null)
                {
                    IsPopupOpenAdd = false;
                    MessageCautionAdd wd= new MessageCautionAdd();
                    wd.ShowDialog();
                    IsPopupOpenAdd = true;
                }
                else
                {
                    if (Description == null) { Description = ""; }
                    Error newerror = new Error
                    {
                        DisplayName = Name,
                        Status = Status,
                        Description = Description,
                        IsDeleted = false,

                    };
                    resetdata();
                    (bool IsAdded, string messageAdd) = await ErrorService.Ins.AddNewError(newerror);
                    if (IsAdded)
                    {
                        ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                         ProList = new List<ErrorDTO>(ProblemList);
                        IsPopupOpenAdd = false;
                       MessageAdd wd = new MessageAdd();
                        wd.ShowDialog();
                    }
                   
                }

            });
            OpenEdit = new RelayCommand<ErrorDTO>((p) => { return true; }, (p)=>
            {
                ExecuteOpenEdit(p);
                Name = SelectedItem.DisplayName;
                Status = SelectedItem.Status;
                Description = SelectedItem.Description;            
            });
            CloseEdit = new RelayCommand<object>((p) => { return true; }, (p) => 
            { 
                IsPopupOpenEdit = false;
                resetdata() ;
               
            });
            Edit=new RelayCommand<object>((p) => { return true; },async (p)=>
            {

                if (Status == "")
                {
                    MessageBox.Show("Bạn nhập chưa đủ dữ liệu");
                }
                else
                {
                    if (Description == null) { Description = ""; }
                    Error newerror = new Error
                    {
                        ID = SelectedItem.ID,
                        DisplayName = SelectedItem.DisplayName,
                        Status = this.Status,
                        Description = this.Description,
                        IsDeleted = false,
                    };

                    (bool success, string messageEdit) = await ErrorService.Ins.EditError(newerror);
                    if (success)
                    {
                        IsPopupOpenEdit = false;
                        ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                        MessageEdit wd = new MessageEdit();
                        wd.ShowDialog();
                        resetdata();
                    }
                }
                                      
            });
            OpenDelete = new RelayCommand<ErrorDTO>((p) => { return true; }, (p) =>
            {
                ExecuteOpenDelete(p);
                IsPopupOpenDelete = true;           

            });
            CloseDelete = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenDelete = false;
            });
            Delete = new RelayCommand<object>((p) => { return true; },async (p) =>
            {
              

                (bool sucess, string messageDelete) = await ErrorService.Ins.DeleteError(SelectedItem.ID);
                if (sucess)
                {
                    ProblemList.Remove(SelectedItem);
                    IsPopupOpenDelete = false;
                    MessageDelete wd =new MessageDelete();
                    wd.ShowDialog();
                }
            });
            Search = new RelayCommand<TextBox>((p) => { return true; },async (p) =>
            {
                if(p.Text == "" )
                {
                    ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                }    
                else
                {
                    ProList = new List<ErrorDTO>(ProblemList);
                    ProblemList = new ObservableCollection<ErrorDTO>(ProList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }    
            });
            CloseMessAdd = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            CloseMessCauAdd = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            CloseMessEdit = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            CloseMessDelete = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
