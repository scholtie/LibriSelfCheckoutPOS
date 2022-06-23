using log4net;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace LibriSelfCheckoutPOS.Views
{
    /// <summary>
    /// Interaction logic for KezdoKepernyoView.xaml
    /// </summary>
    public partial class KezdoKepernyoView : UserControl
    {
        public KezdoKepernyoView()
        {
            InitializeComponent();
            ILog log = LogManager.GetLogger(typeof(App));
            log.Debug(App.User.Name);
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        }
    }
}
