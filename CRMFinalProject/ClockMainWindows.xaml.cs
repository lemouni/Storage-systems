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

namespace CRMFinalProject
{
    /// <summary>
    /// Interaction logic for ClockMainWindows.xaml
    /// </summary>
    public partial class ClockMainWindows : UserControl
    {
        public ClockMainWindows()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }
        private void Timer_Tick(object sender , EventArgs e)
        {
            ClockText.Text = DateTime.Now.ToString();
        }

        
    }
}
