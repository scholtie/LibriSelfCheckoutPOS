using LibriSelfCheckoutPOS.ViewModels;
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
    /// Interaction logic for PromocioView.xaml
    /// </summary>
    public partial class PromocioView : UserControl
    {
        public PromocioView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            char c = '\0';
            if ((e.Key >= Key.A) && (e.Key <= Key.Z))
            {
                c = (char)((int)'a' + (int)(e.Key - Key.A));
            }

            else if ((e.Key >= Key.D0) && (e.Key <= Key.D9))
            {
                c = (char)((int)'0' + (int)(e.Key - Key.D0));
                adminbarCode += c;
            }

            if ((e.Key == Key.Enter))
            {
                adminBarCodeAkt = adminbarCode;
                adminbarCode = "";
                //App.MyItemAdatok result2;
                var vm = (PromocioViewModel)this.DataContext;
                if (vm != null)
                {
                    vm.SearchProduct(adminBarCodeAkt);
                }
            }
        }
        private string adminbarCode = string.Empty;
        public string adminBarCodeAkt;
        public string adminresult;
    }
}
