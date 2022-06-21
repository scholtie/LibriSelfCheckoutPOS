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
            //var lastInput = GetLastInputTime;
            //var startTimeSpan = TimeSpan.Zero;
            //var periodTimeSpan = TimeSpan.FromSeconds(5);

            //var timer = new Timer((e) =>
            //{
            //}, null, startTimeSpan, periodTimeSpan);
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            //this.DataContext = this;
            //DataContext = this;
            //var vm = (CheckOutListViewModel)this.DataContext;
            //if (vm != null)
            //{
            //    Thread thread = new Thread(() =>
            //    {
            //        while (true)
            //        {
            //            try
            //            {
            //                lvDataBinding.ItemsSource = vm.ProductList;
            //            }
            //            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            //        }
            //    });
            //    thread.IsBackground = true;
            //    thread.SetApartmentState(ApartmentState.STA);
            //    thread.Start();
                
            //}
            //lvDataBinding.ItemsSource = vm.ProductList;
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

        void timer_Tick(object sender, EventArgs e)
        {
            var vm = (CheckOutListViewModel)this.DataContext;
            if (vm != null)
            {
                if (GetLastUserInput.GetIdleTickCount() > 30000)
                {
                    log.Debug(GetLastUserInput.GetIdleTickCount());
                    log.Debug(TimeSpan.FromTicks(300000000).TotalSeconds);
                    vm.IdleCommand.Execute(null);
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

            /// <summary>
            /// Idle time in ticks
            /// </summary>
            /// <returns></returns>
            public static uint GetIdleTickCount()
            {
                return ((uint)Environment.TickCount - GetLastInputTime());
            }
            /// <summary>
            /// Last input time in ticks
            /// </summary>
            /// <returns></returns>
            public static uint GetLastInputTime()
            {
                if (!GetLastInputInfo(ref lastInPutNfo))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                return lastInPutNfo.dwTime;
            }
        }

        //[DllImport("user32.dll")]
        //static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        //static uint GetLastInputTime()
        //{
        //    ILog log = LogManager.GetLogger(typeof(App));
        //    uint idleTime = 0;
        //    LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
        //    lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
        //    lastInputInfo.dwTime = 0;

        //    uint envTicks = (uint)Environment.TickCount;

        //    if (GetLastInputInfo(ref lastInputInfo))
        //    {
        //        uint lastInputTick = lastInputInfo.dwTime;

        //        idleTime = envTicks - lastInputTick;
        //    }
        //    return ((idleTime > 0) ? (idleTime / 1000) : 0);
        //}

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

        
    }

}
