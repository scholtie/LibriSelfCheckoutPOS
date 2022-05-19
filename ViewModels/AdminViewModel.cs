using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class AdminViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public ICommand ErtekesitesCommand { get; }
        public ICommand PenztarmuveletekCommand { get; }
        public AdminViewModel(NavigationService startViewNavigationService, NavigationService checkOutListAdminViewNavigationService,
            NavigationService penztarmuveletekAdminViewNavigationService)
        {
            CancelCommand = new NavigateCommand(startViewNavigationService);
            ErtekesitesCommand = new NavigateCommand(checkOutListAdminViewNavigationService);
            PenztarmuveletekCommand = new NavigateCommand(penztarmuveletekAdminViewNavigationService);
        }
    }
}
