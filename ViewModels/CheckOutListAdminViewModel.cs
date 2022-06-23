using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class CheckOutListAdminViewModel : ViewModelBase
    {
        
        private ICommand _torlesCommand;
        public String Harf { get; set; } = new string("1");
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        public ICommand CancelCommand { get; }
        public ICommand CikkKeresesCommand { get; }
        public CheckOutListAdminViewModel(NavigationService checkoutViewNavigationService, NavigationService cikkKeresesViewNavigationService)
        {
            CancelCommand = new NavigateCommand(checkoutViewNavigationService);
            CikkKeresesCommand = new NavigateCommand(cikkKeresesViewNavigationService);
        }

        public ICommand TorlesCommand
        {
            get
            {
                return _torlesCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
            }
        }
        public static bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }
        public ScannedProduct SelectedProduct { get; set; }
        public void MyAction()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.productIsDeleted = true;
            }
        }
    }
}
