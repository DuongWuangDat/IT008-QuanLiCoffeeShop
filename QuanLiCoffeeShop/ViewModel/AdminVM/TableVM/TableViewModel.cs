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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Controls;

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
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        private string genreName;
        public string GenreName
        {
            get { return genreName; }
            set { genreName = value; OnPropertyChanged(); }
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
        private bool isPopupOpenInfo=false;
        public bool IsPopupOpenInfo
        {
            get { return isPopupOpenInfo; }
            set { isPopupOpenInfo = value;
                IsOpenMain = !value;
                OnPropertyChanged(nameof(IsPopupOpenInfo)); }
        }
        void resetdata()
        {
            ID = -1;
            Status=null;
        }
        void get_selecteditem(SeatDTO a)
        {
            SelectedItem = null;
            SelectedItem = a;
        }
        public ICommand FirstLoadTable { get; set; }
        public ICommand Add {  get; set; }
        public ICommand Delete { get; set; }
        public ICommand Edit { get; set; }
        public ICommand OpenEdit {  get; set; }
        public ICommand CloseEdit { get; set;}
        public ICommand OpenDelete { get; set; }

        public ICommand CloseDelete { get; set;}
        public ICommand OpenInfo { get; set; }
        public ICommand CloseInfo { get; set; }
        public TableViewModel()
        {
            FirstLoadTable = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                Tablelist = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
               tablelist = new List<SeatDTO>(Tablelist);
            });
            OpenInfo = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                IsPopupOpenInfo = true;
                ID = SelectedItem.ID;
                GenreName = SelectedItem.GenreName;
                Status = SelectedItem.Status;
            });
            OpenEdit = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                IsPopupOpenEdit = true;
                ID=SelectedItem.ID;
                GenreName =SelectedItem.GenreName;
                Status = SelectedItem.Status;
            });
            OpenDelete = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                IsPopupOpenDelete = true;
            });
            CloseDelete = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                IsPopupOpenDelete = false;
            });
            OpenDelete = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                IsPopupOpenDelete = true;
            });
            OpenDelete = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                IsPopupOpenDelete = true;
            });
            Add=new RelayCommand<object>((p) => { return true;},(p)=>
            {

            });


        }
    }
}