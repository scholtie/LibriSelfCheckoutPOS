using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class BejelentkezesViewModel : ViewModelBase
    {

        public ICommand CancelCommand { get; }
        public ICommand _nextCommand;
        public ICommand KezdoKepernyoCommand { get; }

        public event EventHandler ShutdownStarted;

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ??= new CommandHandler(() => MyAction(), () => CanExecute);
            }
        }
        public static bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MyAction()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
            KezdoKepernyoCommand.Execute(null);
        }

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
        private void txtbox_Leave(object sender, EventArgs e)
        {
            closeOnscreenKeyboard();
        }
        public BejelentkezesViewModel(NavigationService cancelNavigationService, NavigationService kezdoKepernyoNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
            KezdoKepernyoCommand = new NavigateCommand(kezdoKepernyoNavigationService);

            string windir = Environment.GetEnvironmentVariable("windir");

            if (HasTouchInput())
            {
                Process p = new();

                p.StartInfo.FileName = windir + @"\System32\cmd.exe";
                p.StartInfo.Arguments = "/C " + windir + @"\System32\osk.exe";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;

                p.Start();
                p.Dispose();
            }




            static bool HasTouchInput()
            {
                foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
                {
                    //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                    if (tabletDevice.Type == TabletDeviceType.Touch)
                        return true;
                }

                return false;
            }
        }

        


    }
}
