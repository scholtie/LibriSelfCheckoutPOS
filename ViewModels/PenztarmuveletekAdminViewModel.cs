using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class PenztarmuveletekAdminViewModel : ViewModelBase
    {
        public ICommand BefizetesCommand { get; }
        public ICommand KifizetesCommand { get; }
        public ICommand RovancsCommand { get; }
        public ICommand BizonylatokCommand { get; }
        public ICommand HangeroszabalyzoCommand { get; }
        public ICommand BeallitasokCommand { get; }
        public ICommand CancelCommand { get; }
        public PenztarmuveletekAdminViewModel(
            NavigationService cancelViewNavigationService,
            NavigationService befizetesViewNavigationService,
            NavigationService kifizetesViewNavigationService,
            NavigationService rovancsViewNavigationService,
            NavigationService bizonylatokViewNavigationService,
            NavigationService hangeroViewNavigationService,
            NavigationService beallitasokViewNavigationService
            )
        {
            BefizetesCommand = new NavigateCommand(befizetesViewNavigationService);
            KifizetesCommand = new NavigateCommand(kifizetesViewNavigationService);
            RovancsCommand = new NavigateCommand(rovancsViewNavigationService);
            BizonylatokCommand = new NavigateCommand(bizonylatokViewNavigationService);
            HangeroszabalyzoCommand = new NavigateCommand(hangeroViewNavigationService);
            BeallitasokCommand = new NavigateCommand(beallitasokViewNavigationService);
            CancelCommand = new NavigateCommand(cancelViewNavigationService);
        }
    }
}
