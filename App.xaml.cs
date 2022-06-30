using LibriSelfCheckoutPOS.Services;
using LibriSelfCheckoutPOS.Stores;
using LibriSelfCheckoutPOS.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using static LibriSelfCheckoutPOS.Models.DataModels;

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
        //public static Dictionary<int, ScannedProduct> FelvettCikkek = new Dictionary<int, ScannedProduct>();
        //public static Dictionary<int, ScannedProduct> OsszesCikk = new Dictionary<int, ScannedProduct>();
        //public static Dictionary<int, double> OsszAr = new Dictionary<int, double>();
        public static ObservableCollection<ScannedProduct> BookList { get; set; } =
            new ObservableCollection<ScannedProduct>();
        public static ObservableCollection<ScannedProduct> OsszesCikk { get; set; } =
            new ObservableCollection<ScannedProduct>();
        public static User User { get; set; } = new User();
        public static bool IsMessageBoxOpen { get; set; } = false;
        public static ScannedProduct PassedScannedProduct { get; set; } = new ScannedProduct();

        


        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateCheckOutListViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            SetLanguageDictionary("en");
            MainWindow.Show();

            base.OnStartup(e);

            log.Debug("Initialising ...");

            int counter = 0;

            using (var sr = new StreamReader("cikk.txt"))
            {
                string line = null;

                // while it reads a key

                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split(';');

                    OsszesCikk.Add(new ScannedProduct { 
                        productId= counter,
                        productBarcode = line.Split(';')[1].ToString(),
                        productName = line.Split(';')[3].ToString(),
                        productArticleNumber = int.Parse(line.Split(';')[0]),
                        productPrice = double.Parse(line.Split(';')[7]) - 10,
                        productUnitPrice = double.Parse(line.Split(';')[7]),
                        productDiscount = 10});
                    counter++;
                    // add the key and whatever it 
                    // can read next as the value
                    //cikkek.Add(arr[0], arr[1]);
                }
            }
            //log.Debug(OsszesCikk);
        }

        public void SetLanguageDictionary(String language)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (language)
            {
                case "hu":
                    dict.Source = new Uri("..\\Resources\\StringResources.xaml",
                                  UriKind.Relative);
                    break;
                case "de":
                    dict.Source = new Uri("..\\Resources\\StringResources.de.xaml",
                                       UriKind.Relative);
                    break;
                case "en":
                    dict.Source = new Uri("..\\Resources\\StringResource.en.xaml",
                                       UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\StringResource.en.xaml",
                                      UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
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
                new NavigationService(_navigationStore, CreateFizetesViewModel),
                new NavigationService(_navigationStore, CreatePromocioViewModel));
        }


        private StartViewModel CreateCheckOutListViewModel()
        {
            return new StartViewModel(new NavigationService(_navigationStore, CreateBejelentkezesViewModel),
                new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private AdminViewModel CreateAdminViewModel()
        {
            return new AdminViewModel(
                new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel),
                new NavigationService(_navigationStore, CreateCheckOutListAdminViewModel),
                new NavigationService(_navigationStore, CreatePenztarmuveletekAdminViewModel),
                new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private CheckOutListAdminViewModel CreateCheckOutListAdminViewModel()
        {
            return new CheckOutListAdminViewModel(new NavigationService(_navigationStore, CreateAdminViewModel),
                new NavigationService(_navigationStore, CreateCikkKeresesViewModel), new NavigationService(_navigationStore, CreateCheckOutListAdminViewModel),
                new NavigationService(_navigationStore, CreatePromocioViewModel),
                new NavigationService(_navigationStore, CreateArFelulirasViewModel),
                new NavigationService(_navigationStore, CreateEngedmenyadasViewModel));
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
            return new KezdoKepernyoViewModel(
                new NavigationService(_navigationStore, CreatePromocioViewModel),
                new NavigationService(_navigationStore, CreateNapnyitasViewModel),
                new NavigationService(_navigationStore, CreateNapzarasViewModel),
                new NavigationService(_navigationStore, CreateEszkozokViewModel),
                new NavigationService(_navigationStore, CreateBeallitasokNyitoViewModel),
                new NavigationService(_navigationStore, CreateBejelentkezesViewModel));
        }

        private FizetesViewModel CreateFizetesViewModel()
        {
            return new FizetesViewModel(new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel),
                new NavigationService(_navigationStore, CreateFinalViewModel),
                new NavigationService(_navigationStore, CreateAdminViewModel));
        }

        private PromocioIdleViewModel CreatePromocioIdleViewModel()
        {
            return new PromocioIdleViewModel(new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel));
        }

        private BeallitasokNyitoViewModel CreateBeallitasokNyitoViewModel()
        {
            return new BeallitasokNyitoViewModel(new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private EszkozokViewModel CreateEszkozokViewModel()
        {
            return new EszkozokViewModel(new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private NapnyitasViewModel CreateNapnyitasViewModel()
        {
            return new NapnyitasViewModel(new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private NapzarasViewModel CreateNapzarasViewModel()
        {
            return new NapzarasViewModel(new NavigationService(_navigationStore, CreateKezdoKepernyoViewModel));
        }

        private PromocioViewModel CreatePromocioViewModel()
        {
            return new PromocioViewModel(new NavigationService(_navigationStore, CreateMakeCheckOutListViewModel));
        }

        private CikkKeresesViewModel CreateCikkKeresesViewModel()
        {
            return new CikkKeresesViewModel(new NavigationService(_navigationStore, CreateCheckOutListAdminViewModel));
        }

        private FinalViewModel CreateFinalViewModel()
        {
            return new FinalViewModel(new NavigationService(_navigationStore, CreatePromocioViewModel));
        }

        private ArFelulirasViewModel CreateArFelulirasViewModel()
        {
            return new ArFelulirasViewModel(new NavigationService(_navigationStore, CreateCheckOutListAdminViewModel));
        }

        private EngedmenyadasViewModel CreateEngedmenyadasViewModel()
        {
            return new EngedmenyadasViewModel(new NavigationService(_navigationStore, CreateCheckOutListAdminViewModel));
        }
    }
}
