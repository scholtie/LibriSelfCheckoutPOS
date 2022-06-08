using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    /// Interaction logic for BejelentkezesView.xaml
    /// </summary>
    public partial class BejelentkezesView : UserControl
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        private void closeOnscreenKeyboard()
        {
            // retrieve the handler of the window  
            int iHandle = FindWindow("IPTIP_Main_Window", "");
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }
        }
        public BejelentkezesView()
        {
            InitializeComponent();
            //SetLanguageDictionary();
            Loaded += (s, e) => { // only at this point the control is ready
                Window.GetWindow(this) // get the parent window
                      .Closing += (s1, e1) => closeOnscreenKeyboard(); //disposing logic here
            };
        }
        
    }
}
