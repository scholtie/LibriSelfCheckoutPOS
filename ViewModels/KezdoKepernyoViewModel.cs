using LibriSelfCheckoutPOS.Commands;
using LibriSelfCheckoutPOS.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

namespace LibriSelfCheckoutPOS.ViewModels
{
    internal class KezdoKepernyoViewModel : ViewModelBase
    {
        public User user { get; set; } = App.User;
        public String UserName { get; set; } = App.User.Name;
        public int UserPermission { get; set; } = App.User.Permission;
        public bool ButtonEnabled { get; set; } = App.User.IsAdmin;
        public ICommand BelepesCommand { get; }
        public ICommand NapnyitasCommand { get; }
        public ICommand NapzarasCommand { get; }
        public ICommand KikapcsolasCommand { get; }
        public ICommand EszkozokCommand { get; }
        public ICommand BeallitasokCommand { get; }
        public ICommand KilepesCommand { get; }
        public KezdoKepernyoViewModel(
            NavigationService belepesNavigationService,
            NavigationService napnyitasNavigationService,
            NavigationService napzarasNavigationService,
            NavigationService eszkozokNavigationService,
            NavigationService beallitasokNavigationService,
            NavigationService kilepesNavigationService)
        {
            BelepesCommand = new NavigateCommand(belepesNavigationService);
            NapnyitasCommand = new NavigateCommand(napnyitasNavigationService);
            NapzarasCommand = new NavigateCommand(napzarasNavigationService);
            EszkozokCommand = new NavigateCommand(eszkozokNavigationService);
            BeallitasokCommand = new NavigateCommand(beallitasokNavigationService);
            KilepesCommand = new NavigateCommand(kilepesNavigationService);
        }
    }
}
