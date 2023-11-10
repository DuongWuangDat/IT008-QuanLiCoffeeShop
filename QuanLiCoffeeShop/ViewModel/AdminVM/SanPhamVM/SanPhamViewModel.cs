using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Admin.SanPham;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using System.Diagnostics.Eventing.Reader;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.SanPhamVM
{
    public class SanPhamViewModel : BaseViewModel
    {
        public static List<ProductDTO> prdList;
        private ObservableCollection<ProductDTO> _productList;

        public ObservableCollection<ProductDTO> ProductList
        {
            get { return _productList; }
            set {  _productList = value; OnPropertyChanged(); }
        }

        private ProductDTO _selectedItem;

        public ProductDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }


        private ObservableCollection <string> _genreList;
        public ObservableCollection <string> GenreList
        {
            get { return _genreList;}
            set { _genreList = value;OnPropertyChanged(); }
        }

        //Add page
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _count;
        public string Count
        {
            get { return _count; }
            set { _count = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        //Edit page
        private string _editname;
        public string EditName
        {
            get { return _editname; }
            set { _editname = value; }
        }

        private string _editPrice;
        public string EditPrice
        {
            get { return _editPrice; }
            set { _editPrice = value; }
        }

        private string _editGenre;
        public string EditGenre
        {
            get { return _editGenre; }
            set { _editGenre = value;}
        }

        private string _editCount;
        public string EditCount
        {
            get { return _editCount; }
            set { _editCount = value; }
        }

        private string _editDescription;
        public string EditDescription
        {
            get { return _editDescription; }
            set { _editDescription = value;}
        }

        private string _editImage;
        public string EditImage
        {
            get { return _editImage; }
            set { _editImage = value; OnPropertyChanged(); }
        }
        private string OriginImage { get; set; }

        public ICommand FirstLoadCM { get; set; }
        public ICommand SearchSanPhamCM { get; set; }
        public ICommand AddSanPhamCM { get; set; }
        public ICommand AddSanPhamListCM { get; set; }
        public ICommand CloseWdCM { get; set; }
        public ICommand OpenEditWdCM { get; set; }
        public ICommand EditSanPhamListCM { get; set; }
        public ICommand DeleteSanPhamListCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand EditImageCM { get; set; }
        public SanPhamViewModel()
        {
            FirstLoadCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                if (ProductList != null)
                {
                    prdList = new List<ProductDTO>(ProductList);
                }
                GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllPrD());
            });

            SearchSanPhamCM = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if (p.Text == "")
                {
                    ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                }
                else
                {
                    ProductList = new ObservableCollection<ProductDTO>(prdList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }

            });
            AddSanPhamCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddSanPham wd = new AddSanPham();
                wd.ShowDialog();
            });
            AddSanPhamListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (this.Name == null || this.Genre == null || this.Image == null)
                {
                    if(this.Name == null) { MessageBox.Show("1");}
                    if (this.Genre == null) { MessageBox.Show("2"); }
                    if (this.Image == null) { MessageBox.Show("3"); }
                    MessageBox.Show("Không nhập đủ dữ liệu!");
                }
                else
                {
                    int id; 
                    GenreProduct genrePrD;
                    (id, genrePrD) = await GenreService.Ins.FindGenrePrD(Genre);
                    if (this.Description == null) this.Description = "";
                    Product newPrd = new Product
                    {
                        DisplayName = this.Name,
                        Price = decimal.Parse(this.Price),
                        Description = this.Description,
                        IDGenre = id,
                        GenreProduct = genrePrD,
                        Count=int.Parse(this.Count),
                        Image= await CloudService.Ins.UploadImage(this.Image),
                        IsDeleted = false,

                    };
                    (bool IsAdded, string messageAdd) = await ProductService.Ins.AddNewPrD(newPrd);
                    if (IsAdded)
                    {
                        p.Close();
                        ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                    }
                    else
                    {
                        MessageBox.Show(messageAdd);
                    }
                }

            });

            UploadImageCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {                   
                    Image = openFileDialog.FileName;                  
                    if (Image != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBox.Show("Tải ảnh lên thất bại!");
                    }                   
                }

            });

            EditImageCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {              
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {

                    EditImage = openFileDialog.FileName;
                    if (EditImage != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBox.Show("Tải ảnh lên thất bại!");
                    }
                }
            });

            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            OpenEditWdCM = new RelayCommand<object>((p) => { return true; }, (p) => {
                EditName = SelectedItem.DisplayName;
                EditGenre = SelectedItem.GenreName;
                EditPrice = SelectedItem.Price.ToString();
                EditCount = SelectedItem.Count.ToString();
                EditImage = SelectedItem.Image;
                EditDescription = SelectedItem.Description;
                OriginImage = SelectedItem.Image;
                EditSanPham wd = new EditSanPham();
                wd.ShowDialog();
            });

            EditSanPhamListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (this.EditName == null || this.EditGenre == null || this.EditImage == null)
                {
                    MessageBox.Show("Không nhập đủ dữ liệu!");
                }
                else
                {
                    if (this.EditDescription == null) this.EditDescription = "";
                    int id;

                    GenreProduct genrePrD;
                    (id, genrePrD) = await GenreService.Ins.FindGenrePrD(EditGenre);

                    if(OriginImage!=EditImage) 
                        await CloudService.Ins.DeleteImage(OriginImage);
                    Product newPrD = new Product
                    {
                        ID = SelectedItem.ID,
                        DisplayName = EditName,
                        GenreProduct = genrePrD,
                        IDGenre = id,
                        Price = decimal.Parse(EditPrice),
                        Count = int.Parse(EditCount),
                        Image = EditImage,
                        Description = EditDescription,
                        IsDeleted = false,
                    };
                    (bool success, string messageEdit) = await ProductService.Ins.EditPrD(newPrD, SelectedItem.ID);
                    if (success)
                    {
                        p.Close();
                        ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                    }
                    else
                    {
                        MessageBox.Show(messageEdit);
                    }
                }

            });
            DeleteSanPhamListCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                DeleteMessage wd = new DeleteMessage();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    (bool sucess, string messageDelete) = await ProductService.Ins.DeletePrD(SelectedItem.ID);
                    if (sucess)
                    {
                        ProductList.Remove(SelectedItem);
                    }
                    else
                    {
                        MessageBox.Show(messageDelete);
                    }
                }

            });
        }
    }
}
