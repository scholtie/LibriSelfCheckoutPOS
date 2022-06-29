using KasszaWPF;
using LibriSelfCheckoutPOS.ViewModels;
using log4net;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.Views
{
    /// <summary>
    /// Interaction logic for CheckOutListView.xaml
    /// </summary>
    public partial class CheckOutListView : UserControl
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
        public CheckOutListView()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var vm = (CheckOutListViewModel)this.DataContext;
            if (vm != null)
            {
                if (GetLastUserInput.GetIdleTickCount() > 30000)
                {
                    log.Debug(GetLastUserInput.GetIdleTickCount());
                    log.Debug(TimeSpan.FromTicks(300000000).TotalSeconds);
                    if (App.BookList.Count == 0 && App.IsMessageBoxOpen == false)
                    {
                        vm.IdleCommand.Execute(null);
                    }
                    
                }
            }
            
        }

        public class GetLastUserInput
        {
            private struct LASTINPUTINFO
            {
                public uint cbSize;
                public uint dwTime;
            }
            private static LASTINPUTINFO lastInPutNfo;
            static GetLastUserInput()
            {
                lastInPutNfo = new LASTINPUTINFO();
                lastInPutNfo.cbSize = (uint)Marshal.SizeOf(lastInPutNfo);
            }
            [DllImport("User32.dll")]
            private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

            public static uint GetIdleTickCount()
            {
                return ((uint)Environment.TickCount - GetLastInputTime());
            }

            public static uint GetLastInputTime()
            {
                if (!GetLastInputInfo(ref lastInPutNfo))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                return lastInPutNfo.dwTime;
            }
        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
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

                var vm = (CheckOutListViewModel)this.DataContext;
                if (vm != null)
                {
                    vm.SearchProduct(barcode);
                }
                pressedKeys = "";
            }
            else
            {
                pressedKeys += e.Key;
            }
            log.Debug(pressedKeys);
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

        
    }

}
