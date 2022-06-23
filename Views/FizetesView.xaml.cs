using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibriSelfCheckoutPOS.Views
{
    /// <summary>
    /// Interaction logic for FizetesView.xaml
    /// </summary>
    public partial class FizetesView : UserControl
    {
        ILog log = LogManager.GetLogger(typeof(App));
        String barcode;
        public FizetesView()
        {
            InitializeComponent();
            //Box.KeyDown += new KeyEventHandler(tb_KeyDown);
            //SearchProduct();
            //List<Item> items = new List<Item>();
            //items.Add(new Item() { Name = "Könyv1", ArticleNumber=43242, UnitPrice= 4599, Discount=300, Value=4299 });
            //items.Add(new Item() { Name = "Könyv2", ArticleNumber = 4324322, UnitPrice = 5599, Discount = 300, Value = 5299 });
            //items.Add(new Item() { Name = "Könyv3", ArticleNumber = 4324212, UnitPrice = 6599, Discount = 300, Value = 6299 });
            //lvDataBinding.ItemsSource = items;
            //MainWindow win = (MainWindow)Window.GetWindow(this);
            //win.SetLanguageDictionary("en");
        }

        private void SearchProduct(String barcode)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("E:\\EPÁllományok\\cikk.txt");
            while ((line = file.ReadLine()) != null)
            {
                string col = line.Split(';')[1];
                //string colPrice = line.Split(';')[7];
                if (col.Contains(barcode))
                {
                    log.Debug(counter.ToString() + ": " + line);
                    new ScannedProduct { productName = line[3].ToString() , productArticleNumber = line[0] , productPrice = line[7]};
                }

                counter++;
            }

            file.Close();
        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        //private void txtAssetTag_KeyPress(object sender, KeyEventArgs e)

        //{
        //    barcode = Box.Text;

        //    SearchProduct(barcode);

        //}


        //private void tb_KeyDown(object sender, KeyEventArgs e)
        //{
        //    barcode = Box.Text;
        //    if (e.Key == Key.Enter)
        //    {
        //        SearchProduct(barcode);
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
                return this.Name + ", " + this.ArticleNumber + ", " + this.UnitPrice + " Ft" + ", " + this.Discount + " Ft" + ", "  + this.Value + " Ft";
            }
        }

        public class ScannedProduct
        {
            public string productName { get; set; }
            public int productArticleNumber { get; set; }
            public double productPrice { get; set; }

            public override string ToString()
            {
                return this.productName + this.productArticleNumber + this.productPrice;
            }
        }
    }
}
