using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class CheckOutListAdminViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private ICommand _torlesCommand;
        private ICommand _megszakitasCommand;
        private ICommand _felulirasCommand;
        public ICommand ReloadCommand;
        public ICommand PromocioCommand;
        public ICommand ArFelulirasCommand { get; }
        public ICommand EngedmenyCommand { get; }
        //public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        public ICommand CancelCommand { get; }
        public ICommand CikkKeresesCommand { get; }
        public CheckOutListAdminViewModel(NavigationService checkoutViewNavigationService, NavigationService cikkKeresesViewNavigationService, 
            NavigationService reloadNavigationService, NavigationService promocioNavigationService, NavigationService felulirasNavigationService,
            NavigationService engedmenyNavigationService)
        {
            CancelCommand = new NavigateCommand(checkoutViewNavigationService);
            CikkKeresesCommand = new NavigateCommand(cikkKeresesViewNavigationService);
            ReloadCommand = new NavigateCommand(reloadNavigationService);
            PromocioCommand = new NavigateCommand(promocioNavigationService);
            ArFelulirasCommand = new NavigateCommand(felulirasNavigationService);
            EngedmenyCommand = new NavigateCommand(engedmenyNavigationService);
        }

        public ICommand TorlesCommand
        {
            get
            {
                return _torlesCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
            }
        }
        public ICommand MegszakitasCommand
        {
            get
            {
                return _megszakitasCommand ??= new CommandHandler(() => NyugtaMegszakitas(), () => CanExecute);
            }
        }

        public ICommand FelulirasCommand
        {
            get
            {
                return _felulirasCommand ??= new CommandHandler(() => ArFeluliras(), () => CanExecute);
            }
        }

        private void ArFeluliras()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.productPrice = 50;
                RaisePropertyChanged("SelectedProduct");
                ReloadCommand.Execute(null);
            }
        }

        private void NyugtaMegszakitas()
        {
            App.BookList.Clear();
            PromocioCommand.Execute(null);
        }

        public static bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        //public ObservableCollection<ScannedProduct> FelvettCikkek
        //{
        //    get { return felvettCikkek; }
        //    set
        //    {
        //        felvettCikkek = value;
        //        RaisePropertyChanged("FelvettCikkek");
        //    }
        //}

        private ScannedProduct selectedProduct;
        public ScannedProduct SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        //public ScannedProduct SelectedProduct { get; set; }
        public void MyAction()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.productIsDeleted = true;
                RaisePropertyChanged("SelectedProduct");
                ReloadCommand.Execute(null);
            }
        }
    }
}
