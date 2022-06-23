using KasszaWPF;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class PromocioViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<ScannedProduct> FelvettCikkek { get; set; } = new ObservableCollection<ScannedProduct>(App.BookList);
        private Boolean isFound = false;
        private double priceSum = App.BookList.Where(c => c.productIsDeleted == false).Select(c => c.productPrice).Sum();
        int checkoutQuantity = App.BookList.Where(c => c.productIsDeleted == false).Count();
        public ICommand CancelCommand { get; }
        public PromocioViewModel(NavigationService checkoutViewNavigationService)
        {
            CancelCommand = new NavigateCommand(checkoutViewNavigationService);
        }

        int counter = 0;
        public void SearchProduct(String barcode)
        {
            isFound = false;
            if (App.BookList.Count > 0)
            {
                counter = App.BookList.Count + 1;
            }
            else
            {
                counter = 1;
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
                        FelvettCikkek.Add(new ScannedProduct()
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
                    App.PassedScannedProduct = new ScannedProduct()
                    {
                        productId = counter,
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10,
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                        productIsDeleted = false
                    };
                    ProductNameCurrent = line.Split(';')[3].ToString();
                        CheckoutQuantity += 1;
                        ProductArticleNumberCurrent = int.Parse(line.Split(';')[0]);
                        ProductPriceCurrent = double.Parse(line.Split(';')[7]) - 10;
                        //App.OsszAr.Add(counter, double.Parse(line.Split(';')[7]) - 10);
                        var res = App.BookList.Where(c => c.productIsDeleted == false);
                        //PriceSum = App.OsszAr.Values.Sum();
                        PriceSum = res.Select(c => c.productPrice).Sum();
                        CheckoutQuantity = App.BookList.Where(c => c.productIsDeleted == false).Count();
                        //ProductList = App.OsszesCikk.TryGe;
                        //var keys = new List<ScannedProduct>(App.FelvettCikkek.Keys)
                        //ProductList.Add(keys);
                        //log.Debug(ProductList);
                        counter += 1;
                    CancelCommand.Execute(null);
                        //log.Debug(App.OsszAr);
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
    }
}
