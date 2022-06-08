using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class CheckOutListViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private ICommand _clickCommand;
        private bool canPay = true;
        public Boolean PaymentEnabled
        {
            get { return canPay; }
            set
            {
                canPay = value;
                OnPropertyChanged("ButtonEnabled");
            }
        }
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
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
            string windir = Environment.GetEnvironmentVariable("windir");

            Process p = new();

            p.StartInfo.FileName = windir + @"\System32\cmd.exe";
            p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;

            p.Start();
            p.Dispose();

        }

        public ICommand AdminCommand { get; }
        public ICommand FizetesCommand { get; }
        public CheckOutListViewModel(NavigationService adminViewNavigationService, NavigationService fizetesViewNavigationService)
        {
            AdminCommand = new NavigateCommand(adminViewNavigationService);
            FizetesCommand = new NavigateCommand(fizetesViewNavigationService);
        }

    }
}
