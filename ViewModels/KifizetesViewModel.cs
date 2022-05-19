using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System.Windows.Input;
using System.Windows;
using System.Threading;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class KifizetesViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }

        public KifizetesViewModel(NavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
        }
    }
}
