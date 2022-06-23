using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class FizetesViewModel : ViewModelBase
    {
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        private double priceSum = App.BookList.Where(c => c.productIsDeleted == false).Select(c => c.productPrice).Sum();
        private ICommand _languageCommand;
        public ICommand _payCommand;
        private bool canChange = true;

        public ICommand PayCommand
        {
            get
            {
                return _payCommand ??= new CommandHandler(() => PayAndEmptyList(), () => CanExecute);
            }
        }

        public void PayAndEmptyList()
        {
            App.BookList.Clear();
            SuccessCommand.Execute(null);
        }
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

        public double PriceSum
        {
            get { return priceSum; }
            set
            {
                priceSum = value;
            }
        }

        public string getHelp = "Segítségkérés";
        public ICommand CancelCommand { get; }
        public ICommand SuccessCommand { get; }
        public FizetesViewModel(NavigationService cancelNavigationService, NavigationService successNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            SuccessCommand = new NavigateCommand(successNavigationService);
        }
    }
}
