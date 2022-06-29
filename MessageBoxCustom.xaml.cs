using LibriSelfCheckoutPOS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace KasszaWPF
{
    /// <summary>
    /// Interaction logic for MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        private string barcode;
        private string pressedKeys;
        private Boolean isFound = false;
        public MessageBoxCustom(string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            switch (Type)
            {

                case MessageType.Info:
                    txtTitle.Text = "Info";
                    break;
                case MessageType.Confirmation:
                    txtTitle.Text = "Megerősítés";
                    break;
                case MessageType.Success:
                    {
                        txtTitle.Text = "Siker";
                    }
                    break;
                case MessageType.Warning:
                    txtTitle.Text = "Figyelmeztetés";
                    break;
                case MessageType.Help:
                    txtTitle.Text = "Kérjük várjon munkatársunk megérkezéséig";
                    break;
                case MessageType.Error:
                    {
                        txtTitle.Text = "Hiba";
                    }
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.Alert:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
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

            if ((e.Key == Key.Enter) && adminbarCode != "")
            {
                adminBarCodeAkt = adminbarCode;
                adminbarCode = "";
                //App.MyItemAdatok result2;
                SearchProduct(adminBarCodeAkt);
            }
        }
        private string adminbarCode = string.Empty;
        public string adminBarCodeAkt;
        public string adminresult;
        public void SearchProduct(String barcode)
        {
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
                    if (col == "5901299910948" || col == "5999884034445" || col == "1")
                    {
                        this.DialogResult = true;
                        this.Close();
                        isFound = true;
                    }
                    else
                    {
                        isFound = true;
                    }
                }

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

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Help,
        Error,
    }
    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,Alert
    }


}
