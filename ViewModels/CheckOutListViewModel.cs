using KasszaWPF;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class CheckOutListViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<ScannedProduct> ProductList { get; set; } =
            new ObservableCollection<ScannedProduct>();
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        private double priceSum;
        private Boolean isFound = false;
        int checkoutQuantity;
        public ObservableCollection<ScannedProduct> products = new ObservableCollection<ScannedProduct>();
        private ICommand _clickCommand;
        private bool canPay = true;
        //ILog log = LogManager.GetLogger(typeof(App));
        //private string barcode;
        //private string pressedKeys;
        //private double priceSum;
        //private Boolean isFound = false;
        //int checkoutQuantity;
        //ObservableCollection<ScannedProduct> products = new ObservableCollection<ScannedProduct>();
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
        public ICommand IdleCommand { get; }
        public CheckOutListViewModel(NavigationService adminViewNavigationService, NavigationService fizetesViewNavigationService, NavigationService promotionIdleViewNavigationService)
        {
            AdminCommand = new NavigateCommand(adminViewNavigationService);
            FizetesCommand = new NavigateCommand(fizetesViewNavigationService);
            IdleCommand = new NavigateCommand(promotionIdleViewNavigationService);
        }

        int counter = 0;
        public void SearchProduct(String barcode)
        {
            ILog log = LogManager.GetLogger(typeof(App));
            isFound = false;
            if (App.FelvettCikkek.Count > 0)
            {
                counter = App.FelvettCikkek.Count;
            }
            else
            {
                counter = 0;
            }
            string line;
            //int x = 0;

            // Read the file and display it line by line.
            //System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\CJ\\Desktop\\cikk.txt");
            StreamReader file = new StreamReader("cikk.txt");
            while ((line = file.ReadLine()) != null)
            {
                string col = line.Split(';')[1];
                //string colPrice = line.Split(';')[7];
                if (col == barcode)
                {
                    isFound = true;
                    ProductList.Add(new ScannedProduct()
                    {
                        productId = counter,
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10,
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                    });
                    FelvettCikkek.Add( new ScannedProduct()
                    {
                        productId = counter,
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10,
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                        productIsDeleted = false
                    });
                    App.FelvettCikkek.Add(counter, new ScannedProduct()
                    {
                        productId = counter,
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10,
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                        productIsDeleted = false
                    });
                    App.BookList.Add(new ScannedProduct()
                    {
                        productId = counter,
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10,
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                        productIsDeleted = false
                    });
                    ProductNameCurrent = line.Split(';')[3].ToString();
                    CheckoutQuantity += 1;
                    ProductArticleNumberCurrent = int.Parse(line.Split(';')[0]);
                    ProductPriceCurrent = double.Parse(line.Split(';')[7]) - 10;
                    App.OsszAr.Add(counter, double.Parse(line.Split(';')[7]) - 10);
                    PriceSum = App.OsszAr.Values.Sum();
                    CheckoutQuantity = App.OsszAr.Values.Count();
                    //ProductList = App.OsszesCikk.TryGe;
                    //var keys = new List<ScannedProduct>(App.FelvettCikkek.Keys)
                    //ProductList.Add(keys);
                    //log.Debug(ProductList);
                    log.Debug(App.BookList);
                    counter+=1;
                    log.Debug(App.OsszAr);
                }

            }
            if (isFound == false)
            {
                bool? Result = new MessageBoxCustom("Termék nem található!", MessageType.Info, MessageButtons.Ok).ShowDialog();

                if (Result.Value)
                {
                    //Application.Current.Shutdown();
                }
            }

            file.Close();

        }

        private string productNameCurrent;
        private double productPriceCurrent;
        private int productArticleNumberCurrent;
        public string ProductNameCurrent
        {
            get { return productNameCurrent; }
            set
            {
                productNameCurrent = value;
                RaisePropertyChanged("ProductNameCurrent");
            }
        }
        public int ProductArticleNumberCurrent
        {
            get { return productArticleNumberCurrent; }
            set
            {
                productArticleNumberCurrent = value;
                RaisePropertyChanged("ProductArticleNumberCurrent");
            }
        }
        public double ProductPriceCurrent
        {
            get { return productPriceCurrent; }
            set
            {
                productPriceCurrent = value;
                RaisePropertyChanged("ProductPriceCurrent");
            }
        }

        public double PriceSum
        {
            get { return priceSum; }
            set
            {
                priceSum = value;
                RaisePropertyChanged("PriceSum");
            }
        }

        public int CheckoutQuantity
        {
            get { return checkoutQuantity; }
            set
            {
                checkoutQuantity = value;
                RaisePropertyChanged("CheckoutQuantity");
            }
        }


        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //private void txtAssetTag_KeyPress(object sender, KeyEventArgs e)

        //{
        //    barcode = Box.Text;

        //    SearchProduct(barcode);

        //}


        //private void tb_KeyDown(object sender, KeyEventArgs e)
        //{
        //    log.Debug(e.Key);
        //    barcode = Box.Text;
        //    if (e.Key == Key.Enter)
        //    {
        //        SearchProduct(barcode);
        //        Box.Text = "";
        //    }
        //}

        public class Item
        {
            public string Name { get; set; }
            public int ArticleNumber { get; set; }
            public double UnitPrice { get; set; }
            public double Discount { get; set; }
            public double Value { get; set; }

            public override string ToString()
            {
                return Name + ", " + ArticleNumber + ", " + UnitPrice + " Ft" + ", " + Discount + " Ft" + ", " + Value + " Ft";
            }
        }

        

        

    }
}
