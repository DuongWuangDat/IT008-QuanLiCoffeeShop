using QuanLiCoffeeShop.ViewModel;

namespace QuanLiCoffeeShop.View.Admin
{
    internal class ButtonViewModel : BaseViewModel
    {
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public RelayCommand<object> ButtonClickCommand { get; }

        public ButtonViewModel()
        {
            IsSelected = false;
            ButtonClickCommand = new RelayCommand<object>(null, ExecuteButtonClick);
        }
        private void ExecuteButtonClick(object parameter)
        {
            IsSelected = true;
        }
    }

}
