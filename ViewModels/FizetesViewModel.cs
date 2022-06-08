using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class FizetesViewModel : ViewModelBase
    {
        private ICommand _languageCommand;
        private bool canChange = true;
        public Boolean PaymentEnabled
        {
            get { return canChange; }
            set
            {
                canChange = value;
                OnPropertyChanged("ButtonEnabled");
            }
        }
        public ICommand LanguageCommand
        {
            get
            {
                return _languageCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
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
            var instance = new MainWindow();
            instance.SetLanguageDictionary("en");

        }

        public string getHelp = "Segítségkérés";
        public ICommand CancelCommand { get; }
        public FizetesViewModel(NavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
        }
    }
}
