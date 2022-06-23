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
    internal class FinalViewModel:ViewModelBase
    {
        public ICommand CancelCommand { get; }

        public FinalViewModel(NavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
        }
    }
}
