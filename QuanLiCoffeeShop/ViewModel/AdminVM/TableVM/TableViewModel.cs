using System;
using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.Table;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.TableVM
{
    internal class TableViewModel: BaseViewModel
    {
        public static List<SeatDTO> tablelist = new List<SeatDTO>();
        private ObservableCollection<SeatDTO> _tablelist = new ObservableCollection<SeatDTO>();
        public ObservableCollection<SeatDTO> Tablelist
        { 
            get { return _tablelist; }
            set {  _tablelist = value; OnPropertyChanged(); }
        }
        private SeatDTO selecteditem;
        public SeatDTO SelectedItem
        {
            get { return selecteditem; }
            set { selecteditem = value; OnPropertyChanged();}
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        private string status;
        public string Status
        {
            get { return status; }  
            set { status = value; OnPropertyChanged(); }
        }
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
        void resetdata()
        {
            Name = null;
            Status=null;
        }
        public ICommand FirstLoadTable { get; set; }
        public ICommand OpenEdit {  get; set; }
        public ICommand CloseEdit { get; set;}
        public ICommand OpenDelete { get; set; }
        public ICommand CloseDelete { get; set;}
    }
}
