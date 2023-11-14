using System;
using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.Table;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLiCoffeeShop.View.MessageBox;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Controls;
using System.Windows;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.TableVM
{
    internal class TableViewModel: BaseViewModel
    {
        public static List<SeatDTO> tablelist = new List<SeatDTO>();
        private ObservableCollection<SeatDTO> _tablelist = new ObservableCollection<SeatDTO>();
        public ObservableCollection<SeatDTO> TableList
        { 
            get { return _tablelist; }
            set {  _tablelist = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _genreList;
        public ObservableCollection<string> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged(); }
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
        public string Genre ;
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
     
        private bool isOpenMain = true;
        public bool IsOpenMain 
        { get { return isOpenMain; }
           set { isOpenMain = value; OnPropertyChanged(nameof(IsOpenMain)); } 
        }
        private bool isPopupOpenAdd = false;
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
            GenreName = null;
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
        public ICommand OpenAdd { get; set; }
        public ICommand CloseAdd { get; set; }
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
                
                TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                tablelist = new List<SeatDTO>(TableList);
                GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllSeat());
                
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
                ID=SelectedItem.ID;
                GenreName =SelectedItem.GenreName;
                Status = SelectedItem.Status;
                IsPopupOpenEdit = true;
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
            OpenAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                resetdata();
                IsPopupOpenAdd = true;
            });
            CloseAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenAdd = false;
            });

            Add =new RelayCommand<object>((p) => { return true;},async (p)=>
            {
                if(Status==null || GenreName==null)
                {
                    MessageBox.Show("nhập thiếu thông tin");
                }
                else
                {
                    int id;
                    GenreSeat genreseat = new GenreSeat();
                    (id, genreseat) = await GenreService.Ins.FindGenreSeat(genreName);
                    Seat newseat = new Seat
                    {
                        Status = Status,
                        IDGenre = id,                        
                    };
                    resetdata();
                    (bool IsAdded, string messageAdd) = await SeatService.Ins.AddNewSeat(newseat);
                    if(IsAdded)
                    {
                        TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                        tablelist = new List<SeatDTO>(TableList);
                        IsPopupOpenAdd = false;
                        MessageBox.Show("Thêm thành công");
                    }    
                    else
                    {
                        MessageBox.Show("Thêm thất bại");                   }    
                }    
            });
            Edit = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Seat newseat = new Seat
                {
                    Status = Status,
                    IDGenre = id,
                };
                (bool success, string messageEdit) = await SeatService.Ins.EditSeat(newseat);
                if(success)
                {
                    IsPopupOpenEdit = false;
                    TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                    tablelist = new List<SeatDTO>(TableList);
                    MessageBox.Show("Sửa thành công");
                    resetdata();
                }
                else
                {
                    MessageBox.Show("Sửa thất bại");
                }

            });


        }
    }
}