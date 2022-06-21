using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class NapnyitasViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public NapnyitasViewModel(NavigationService checkoutViewNavigationService)
        {
            CancelCommand = new NavigateCommand(checkoutViewNavigationService);
        }
    }
}
