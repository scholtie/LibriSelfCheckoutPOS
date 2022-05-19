using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class CheckOutListAdminViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public CheckOutListAdminViewModel(NavigationService checkoutViewNavigationService)
        {
            CancelCommand = new NavigateCommand(checkoutViewNavigationService);
        }
    }
}
