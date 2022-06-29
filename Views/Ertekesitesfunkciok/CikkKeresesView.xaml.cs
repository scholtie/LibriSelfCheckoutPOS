using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.Views.Ertekesitesfunkciok
{
    /// <summary>
    /// Interaction logic for CikkKeresesView.xaml
    /// </summary>
    public partial class CikkKeresesView : UserControl
    {
        public CikkKeresesView()
        {
            InitializeComponent();
        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox1.Focus();
        }

        //private void Textbox_Gotfocus(object sender, RoutedEventArgs e)
        //{

        //    Process p = new();
        //    string windir = Environment.GetEnvironmentVariable("windir");

        //    p.StartInfo.FileName = windir + @"\System32\cmd.exe";
        //        p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
        //        p.StartInfo.CreateNoWindow = true;
        //        p.StartInfo.UseShellExecute = false;


        //        p.Start();
        //        p.Dispose();
        //}
    }
}
