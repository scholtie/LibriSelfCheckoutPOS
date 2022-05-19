using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class KezdoKepernyoViewModel : ViewModelBase
    {
        public ICommand BelepesCommand { get; }
        public KezdoKepernyoViewModel(NavigationService belepesNavigationService)
        {
            BelepesCommand = new NavigateCommand(belepesNavigationService);
        }
    }
}
