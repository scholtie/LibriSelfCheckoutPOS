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
        private ICommand _closeCommand;
        private bool canClose = true;
        public Boolean PaymentEnabled
        {
            get { return canClose; }
            set
            {
                canClose = value;
                OnPropertyChanged("ButtonEnabled");
            }
        }
        public AdminViewModel(NavigationService startViewNavigationService, NavigationService checkOutListAdminViewNavigationService,
            NavigationService penztarmuveletekAdminViewNavigationService)
        {
            CancelCommand = new NavigateCommand(startViewNavigationService);
            ErtekesitesCommand = new NavigateCommand(checkOutListAdminViewNavigationService);
            PenztarmuveletekCommand = new NavigateCommand(penztarmuveletekAdminViewNavigationService);

        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
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

        public static void MyAction()
        {
            //Process.Start("osk.exe");
            System.Windows.Application.Current.Shutdown();

        }

    }
}
