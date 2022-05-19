using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using LibriSelfCheckoutPOS.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class StartViewModel : ViewModelBase
    {
        public string WelcomeText { get; set; }
        public ICommand StartCheckoutCommand { get; }
        public StartViewModel(NavigationService checkoutViewNavigationService)
        {
            StartCheckoutCommand = new NavigateCommand(checkoutViewNavigationService);
        }
    }
}
