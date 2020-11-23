using BaseClass;
using BaseClass.DataAccess;
using BaseClass.ModelDto;
using MaterialDesignThemes.Wpf;
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
using Views.UserControls;
using Views.ViewModel;
using Views.WCustomer;
using Views.WHall;
using Views.WMovie;
using Views.WProjection;
using Views.WUser;

namespace Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User userLogged;
        WorkUser wk;
        UserDto userDto;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User user)
        {
            InitializeComponent();
             userDto = UserDto.Instance;
            wk = new WorkUser();
            userLogged = user;
            ShowUserInfo(user);
            userDto.Fullname = userLogged.Fullname;
            userDto.Id = userLogged.Id;
            var menuAdmin = new List<SubItem>();
            menuAdmin.Add(new SubItem("Usuarios", new ABMUsers()));
            //menuAdmin.Add(new SubItem("Listado Usuarios", new ListUsers()));
            menuAdmin.Add(new SubItem("Peliculas", new ListCardMovies()));
            //menuAdmin.Add(new SubItem("Butacas", new Seats()));
            menuAdmin.Add(new SubItem("Salas", new HallFactory()));
            menuAdmin.Add(new SubItem("Proyeciones", new Projections(userLogged)));

            var itemAdmin = new ItemMenu("Registros", menuAdmin, PackIconKind.Register);

            var menuVendor = new List<SubItem>();
            menuVendor.Add(new SubItem("Clientes", new CRUDCustomer())); // que se cree un UC para clientes
            menuVendor.Add(new SubItem("Proyeciones - Venta Tickets", new Projections(userLogged)));

            var itemVendor = new ItemMenu("Ventas", menuVendor, PackIconKind.Sale);

            if (user.RolId == 1)
            {
                Menu.Children.Add(new UserControlMenuItem(itemAdmin, this));
                StackPanelMain.Children.Add(new ABMUsers());
            } else if (user.RolId == 2)
            {
                Menu.Children.Add(new UserControlMenuItem(itemVendor, this));
                StackPanelMain.Children.Add(new CRUDCustomer());
            }

        }

        public void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
        
        private void ShowUserInfo(User user)
        {
            txtUserInfo.Text = user.Fullname;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            LoginCine login = new LoginCine();
            login.Show();
            this.Close();
            userLogged.Remember = false;
            wk.UpdateWhenIsRemember(userLogged);
            

        }
    
        private void ChangeMenu()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var seat = new Seats();
            //seat.Show();


        }

        
    }
}
