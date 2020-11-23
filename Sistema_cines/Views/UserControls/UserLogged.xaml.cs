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
using BaseClass;

namespace Views.UserControls
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        public UserControl1()
        {
            InitializeComponent();
        }
        private void LogOut(object sender, RoutedEventArgs e)
        {
            var ventanaPadre = Window.GetWindow(this);
            ventanaPadre.Close();
            LoginCine login = new LoginCine();
            login.Show();
        }
    }
}
