using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class CikkKeresesViewModel:ViewModelBase, INotifyPropertyChanged
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;
        
        private ICollectionView phrasesView;
        private string filter;
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.OsszesCikk);
        public ICommand CancelCommand { get; }
        public ICommand _addProductCommand;
        string windir = Environment.GetEnvironmentVariable("windir");
        Process p = new();
        public CikkKeresesViewModel(NavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            phrasesView = CollectionViewSource.GetDefaultView(FelvettCikkek);
            phrasesView.Filter = o => String.IsNullOrEmpty(Filter) ? true : ((ScannedProduct)o).productName.Contains(Filter) || 
            ((ScannedProduct)o).productBarcode.Contains(Filter);

            

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

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (value != filter)
                {
                    filter = value;
                    phrasesView.Refresh();
                    RaisePropertyChanged("Filter");
                }
            }
        }

        public ICommand AddProductCommand
        {
            get
            {
                return _addProductCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
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

        public ScannedProduct SelectedProduct { get; set; }

        int counter;
        public void MyAction()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
            if (App.BookList.Count > 0)
            {
                counter = App.BookList.Count + 1;
            }
            else
            {
                counter = 1;
            }
            if (SelectedProduct != null)
            {
                App.BookList.Add(new ScannedProduct
                {
                    productId=counter,
                    productBarcode = SelectedProduct.productBarcode,
                    productArticleNumber =SelectedProduct.productArticleNumber,
                    productDiscount=SelectedProduct.productDiscount,
                    productIsDeleted=false,
                    productName=SelectedProduct.productName,
                    productPrice=SelectedProduct.productPrice,
                    productUnitPrice=SelectedProduct.productUnitPrice
                });
                CancelCommand.Execute(null);
            }
        }
    }
}
