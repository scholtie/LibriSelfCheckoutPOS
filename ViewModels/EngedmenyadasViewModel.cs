using KasszaWPF;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class EngedmenyadasViewModel:ViewModelBase, INotifyPropertyChanged
    {
        public ICommand CancelCommand { get; }
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        string windir = Environment.GetEnvironmentVariable("windir");
        Process p = new();
        public EngedmenyadasViewModel(NavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            if (HasTouchInput())
            {

                p.StartInfo.FileName = windir + @"\System32\cmd.exe";
                p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;


                p.Start();
                p.Dispose();
            }
        }

        static bool HasTouchInput()
        {
            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }

            return false;
        }

        private string textboxPrice;
        public string TextboxPrice
        {
            get { return textboxPrice; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(textboxPrice, value))
                {
                    textboxPrice = value;
                    RaisePropertyChanged("TextboxPrice"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

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

        private ICommand _overwriteCommand;

        public ICommand OverwriteCommand
        {
            get
            {
                return _overwriteCommand ??= new CommandHandler(() => ArFeluliras(), () => CanExecute);
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

        private void ArFeluliras()
        {
            if (SelectedProduct != null)
            {
                try
                {
                    if (SelectedProduct.productUnitPrice - int.Parse(textboxPrice) >= 0)
                    {
                        SelectedProduct.productDiscount = int.Parse(textboxPrice);
                        SelectedProduct.productPrice = SelectedProduct.productUnitPrice - int.Parse(textboxPrice);
                        RaisePropertyChanged("SelectedProduct");
                        CancelCommand.Execute(null);
                        foreach (var process in Process.GetProcessesByName("osk"))
                        {
                            process.Kill();
                        }
                    }
                    else
                    {
                        bool? Result = new MessageBoxCustom("Az érték nem lehet kisebb mint 0!", MessageType.Info, MessageButtons.Ok).ShowDialog();

                        if (Result.Value)
                        {
                            //Application.Current.Shutdown();
                        }
                    }
                    
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
