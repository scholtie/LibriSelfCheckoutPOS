using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class BejelentkezesViewModel : ViewModelBase
    {

        public ICommand CancelCommand { get; }
        public ICommand KezdoKepernyoCommand { get; }
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
