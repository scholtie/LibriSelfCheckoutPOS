using KasszaWPF;
using LibriSelfCheckoutPOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        private string pressedKeys;
        private string barcode;
        public StartView()
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
                var vm = (StartViewModel)this.DataContext;
                if (vm != null)
                {
                    vm.SearchProduct(barcode);
                }
                
                //Box.Text = "";
                pressedKeys = "";
            }
            else
            {
                pressedKeys += e.Key;
            }
            //barcode = e.Key.ToString();
            barcode = pressedKeys;
        }

        
    }
}
