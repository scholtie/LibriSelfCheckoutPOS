using LibriSelfCheckoutPOS.Services;
using LibriSelfCheckoutPOS.Stores;
using LibriSelfCheckoutPOS.ViewModels;
using log4net;
using System.Windows;
using System.Windows.Input;

namespace LibriSelfCheckoutPOS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        ILog log = LogManager.GetLogger(typeof(App));

        public App()
        {
            _navigationStore = new NavigationStore();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateCheckOutListViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);

            log.Debug("Initialising ...");
        }


        public static bool HasTouchInput()
        {
            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }

            return false;
        }

        private CheckOutListViewModel CreateMakeCheckOutListViewModel()
        {
            return new CheckOutListViewModel(
                new NavigationService(_navigationStore, CreateAdminViewModel),
                new NavigationService(_navigationStore, CreateFizetesViewModel));
        }


        private StartViewModel CreateCheckOutListViewModel()
        {
            return new StartViewModel(new NavigationService(_navigationStore, CreateBejelentkezesViewModel));
        }

        private AdminViewModel CreateAdminViewModel()
        {
            return new AdminViewModel(
                new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel),
                new NavigationService(_navigationStore, CreateCheckOutListAdminViewModel),
                new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel));
        }

        private CheckOutListAdminViewModel CreateCheckOutListAdminViewModel()
        {
            return new CheckOutListAdminViewModel(new NavigationService(_navigationStore, CreateAdminViewModel));
        }

        private BejelentkezesViewModel CreateBejelentkezesViewModel()
        {
            return new BejelentkezesViewModel(new NavigationService(_navigationStore, CreateCheckOutListViewModel),
                new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private PenztarmuveletekAdminViewModel CreatePenztarmuveletekAdminViewModel()
        {
            return new PenztarmuveletekAdminViewModel(
                new NavigationService(_navigationStore, CreateAdminViewModel),
                new NavigationService(_navigationStore, CreateBefizetesViewModel),
                new NavigationService(_navigationStore, CreateKifizetesViewModel),
                new NavigationService(_navigationStore, CreateRovancsViewModel),
                new NavigationService(_navigationStore, CreateBizonylatViewModel),
                new NavigationService(_navigationStore, CreateHangeroszabalyzoiewModel),
                new NavigationService(_navigationStore, CreateBeallitasokViewModel));
        }

        private BefizetesViewModel CreateBefizetesViewModel()
        {
            return new BefizetesViewModel(new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel));
        }

        private KifizetesViewModel CreateKifizetesViewModel()
        {
            return new KifizetesViewModel(new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel));
        }

        private RovancsViewModel CreateRovancsViewModel()
        {
            return new RovancsViewModel(new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel));
        }

        private BizonylatokViewModel CreateBizonylatViewModel()
        {
            return new BizonylatokViewModel(new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel));
        }

        private HangeroszabalyzoViewModel CreateHangeroszabalyzoiewModel()
        {
            return new HangeroszabalyzoViewModel(new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel));
        }

        private BeallitasokViewModel CreateBeallitasokViewModel()
        {
            return new BeallitasokViewModel(new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel),
                new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private KezdoKepernyoViewModel CreateKezdoKepernyoViewModel()
        {
            return new KezdoKepernyoViewModel(new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel));
        }

        private FizetesViewModel CreateFizetesViewModel()
        {
            return new FizetesViewModel(new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel));
        }

        private PromocioIdleViewModel CreatePromocioIdleViewModel()
        {
            return new PromocioIdleViewModel(new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel));
        }
    }
}
