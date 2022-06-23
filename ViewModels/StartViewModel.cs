using KasszaWPF;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using LibriSelfCheckoutPOS.Stores;
using log4net;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class StartViewModel : ViewModelBase
    {
        private Boolean isFound = false;
        User user = new User();
        public string WelcomeText { get; set; }
        public ICommand StartCheckoutCommand { get; }
        public ICommand LoginCommand { get; }

        public StartViewModel(NavigationService checkoutViewNavigationService, NavigationService promotionIdleViewNavigationService)
        {
            StartCheckoutCommand = new NavigateCommand(checkoutViewNavigationService);

            LoginCommand = new NavigateCommand(promotionIdleViewNavigationService);
        }

        public void SearchProduct(string barcode)
        {
            isFound = false;
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
                    if (barcode == "5901299910948" || barcode == "5999884034445" || barcode == "1")
                    {
                        var permission = int.Parse(line.Split(';')[14]);
                        var isAdmin = false;
                        if (permission > 1)
                        {
                            isAdmin = true;
                        }
                        else
                        {
                            isAdmin = false;
                        }
                        user = new User() { Name = line.Split(';')[3].ToString(), Permission = permission, IsAdmin=isAdmin };
                        //line.Split(';')[2].ToString()
                        //int.Parse(line.Split(';')[13])
                        App.User = user;
                        LoginCommand.Execute(null);
                    }

                }

                counter++;
            }
            if (isFound == false)
            {
                bool? Result = new MessageBoxCustom("Alkalmazott nem található!", MessageType.Info, MessageButtons.Ok).ShowDialog();

                if (Result.Value)
                {
                    //Application.Current.Shutdown();
                }
            }

            file.Close();

        }

        
    }
}
