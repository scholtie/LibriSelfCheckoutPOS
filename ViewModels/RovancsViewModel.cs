﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class RovancsViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public RovancsViewModel(NavigationService cancelNavigationService)
        {
            CancelCommand = new NavigateCommand(cancelNavigationService);
        }
    }
}
