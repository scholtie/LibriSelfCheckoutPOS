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
    internal class BeallitasokViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public ICommand KezdoKepernyoCommand { get; }
        public BeallitasokViewModel(NavigationService cancelNavigationService, NavigationService kezdoKepernyoNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            KezdoKepernyoCommand = new NavigateCommand(kezdoKepernyoNavigationService);
        }
    }
}
