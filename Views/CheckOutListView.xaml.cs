using KasszaWPF;
using log4net;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.Views
{
    /// <summary>
    /// Interaction logic for CheckOutListView.xaml
    /// </summary>
    public partial class CheckOutListView : UserControl, INotifyPropertyChanged
    {
        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        ILog log = LogManager.GetLogger(typeof(App));
        private string barcode;
        private string pressedKeys;
        private double priceSum;
        private Boolean isFound = false;
        int checkoutQuantity;
        ObservableCollection<ScannedProduct> products = new ObservableCollection<ScannedProduct>();
        public CheckOutListView()
        {
            var lastInput = GetLastInputTime;
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(5);

            var timer = new Timer((e) =>
            {
            }, null, startTimeSpan, periodTimeSpan);
            InitializeComponent();
            //this.DataContext = this;
            DataContext = this;
            lvDataBinding.ItemsSource = products;
            //IdleTimeFinder.GetIdleTime();
            //while (!Console.KeyAvailable)
            //{
            //    //Thread.Sleep(500);
            //    idle = GetIdleTime();
            //    log.Debug(idle);
            //    if (idle > 500)
            //    {
            //        Application.Current.Shutdown();
            //    }
            //}
            //Box.KeyDown += new KeyEventHandler(tb_KeyDown);
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        static uint GetLastInputTime()
        {
            ILog log = LogManager.GetLogger(typeof(App));
            uint idleTime = 0;
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            uint envTicks = (uint)Environment.TickCount;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = lastInputInfo.dwTime;

                idleTime = envTicks - lastInputTick;
            }
            return ((idleTime > 0) ? (idleTime / 1000) : 0);
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            //pressedKeys += e.Key;
            if (e.Key == Key.D0)
            {
                pressedKeys += "0";
            }
            else if (e.Key == Key.D1)
            {
                pressedKeys += "1";
            }
            else if (e.Key == Key.D2)
            {
                pressedKeys += "2";
            }
            else if (e.Key == Key.D3)
            {
                pressedKeys += "3";
            }
            else if (e.Key == Key.D4)
            {
                pressedKeys += "4";
            }
            else if (e.Key == Key.D5)
            {
                pressedKeys += "5";
            }
            else if (e.Key == Key.D6)
            {
                pressedKeys += "6";
            }
            else if (e.Key == Key.D7)
            {
                pressedKeys += "7";
            }
            else if (e.Key == Key.D8)
            {
                pressedKeys += "8";
            }
            else if (e.Key == Key.D9)
            {
                pressedKeys += "9";
            }
            else if (e.Key == Key.Enter)
            {
                isFound = false;
                SearchProduct(barcode);
                //Box.Text = "";
                pressedKeys = "";
            }
            else
            {
                pressedKeys += e.Key;
            }
            log.Debug(pressedKeys);
            //barcode = e.Key.ToString();
            barcode = pressedKeys;
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void grid_TextInput(object sender, TextCompositionEventArgs e)
        {
            MessageBox.Show(e.Text);
        }

        private void SearchProduct(String barcode)
        {
            int counter = 0;
            string line;
            //int x = 0;

            // Read the file and display it line by line.
            //System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\CJ\\Desktop\\cikk.txt");
            System.IO.StreamReader file = new System.IO.StreamReader("cikk.txt");
            while ((line = file.ReadLine()) != null)
            {
                string col = line.Split(';')[1];
                //string colPrice = line.Split(';')[7];
                if (col == barcode)
                {
                    isFound = true;
                    log.Debug(counter.ToString() + ": " + line);
                    products.Add(new ScannedProduct()
                    {
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10,
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                    });
                    ProductNameCurrent = line.Split(';')[3].ToString();
                    CheckoutQuantity += 1;
                    ProductArticleNumberCurrent = int.Parse(line.Split(';')[0]);
                    ProductPriceCurrent = double.Parse(line.Split(';')[7]) - 10;
                    PriceSum += productPriceCurrent;
                    log.Debug(products);
                }

                counter++;
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

        public class ScannedProduct
        {
            public string productName { get; set; }
            public int productArticleNumber { get; set; }
            public double productPrice { get; set; }
            public double productUnitPrice { get; set; }
            public double productDiscount { get; set; }

            public override string ToString()
            {
                return productName + ", " + productArticleNumber + ", " + productPrice + productUnitPrice + productDiscount;
            }

        }
    }

}
