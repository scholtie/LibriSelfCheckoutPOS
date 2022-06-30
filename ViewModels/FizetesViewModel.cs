using KasszaWPF;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        public ICommand _helpCommand;
        private bool canChange = true;

        public ICommand PayCommand
        {
            get
            {
                return _payCommand ??= new CommandHandler(() => PayAndEmptyList(), () => CanExecute);
            }
        }
        public ICommand HelpCommand
        {
            get
            {
                return _helpCommand ??= new CommandHandler(() => HelpAction(), () => CanExecute);
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
            //var instance = new App();
            //instance.SetLanguageDictionary("hu");
        }


        public void HelpAction()
        {
            App.IsMessageBoxOpen = true;
            bool? Result = new MessageBoxCustom("Kérjük várjon munkatársunk megérkezéséig", MessageType.Warning, MessageButtons.Alert).ShowDialog();

            if (Result.Value)
            {
                AdminCommand.Execute(null);
                App.IsMessageBoxOpen = false;
            }
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
        public ICommand AdminCommand { get; }
        public FizetesViewModel(NavigationService cancelNavigationService, NavigationService successNavigationService, NavigationService adminNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            SuccessCommand = new NavigateCommand(successNavigationService);
            AdminCommand = new NavigateCommand(adminNavigationService);
        }

        public static event EventHandler LanguageChanged;

        protected virtual void OnLanguageChanged(EventArgs e)
        {
            LanguageChanged?.Invoke(this, e);
        }

        public ICommand EnglishLanguageCmd { get { return new RelayCommand(p => SetLanguageToEnglish()); } }

        public ICommand HungarianLanguageCmd { get { return new RelayCommand(p => SetLanguageToHungarian()); } }

        private void SetLanguageToEnglish()
        {
            AppSettings.AppLanguage = "English";
            OnLanguageChanged(EventArgs.Empty);
            UpdateLocalizedElements();
            AdminCommand.Execute(null);
        }

        private void SetLanguageToHungarian()
        {
            AppSettings.AppLanguage = "Hungarian";
            OnLanguageChanged(EventArgs.Empty);
            UpdateLocalizedElements();
        }

        private void UpdateLocalizedElements()
        {
            LocalizationProvider.UpdateAllObjects();
        }
    }
}
