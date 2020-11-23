using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Views.Welcome
{
    /// <summary>
    /// Lógica de interacción para Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            
            InitializeComponent();
            dt.Tick += new EventHandler(dtTick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Start();
        }

        DispatcherTimer dt = new DispatcherTimer();

        private void dtTick(object sender, EventArgs e)
        {
            barra.Width += 2; 
            if (barra.Width >= 310)
            {
                dt.Stop();
                LoginCine l = new LoginCine();
                l.Show();
                this.Close();
            }
            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        

    }
}
