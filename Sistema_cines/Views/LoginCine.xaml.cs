using BaseClass;
using BaseClass.DataAccess;
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
using System.Windows.Shapes;
using Views.ViewModel;

namespace Views
{
    /// <summary>
    /// Lógica de interacción para LoginCine.xaml
    /// </summary>
    public partial class LoginCine : Window
    {
        WorkLogin workLogin;
        WorkUser workUser;
        User user;


        public LoginCine()
        {
            InitializeComponent();
            workLogin = new WorkLogin();
            workUser = new WorkUser();
            user = new User();
        }

        //Boton para acceder a la sesion
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (workLogin.LoginUser(txtUsername.Text, txtPassword.Password))

            {

                User logged = workLogin.getUserLogged();
                RememberUser(logged);
                userLogged(logged);
                ShortNotifications.ShowDialog("Bienvenido " + workLogin.getUserLogged().Fullname);
            }
            else
            {
                ShortNotifications.ShowDialog("Usuario o contraseña incorrecta");
            }
        }

        private void userLogged(User user)
        {
            MainWindow menu = new MainWindow(user);
            menu.Show();
            Window.GetWindow(this).Close();

        }


        private void RememberUser(User u)
        {
            if (remember.IsChecked == true)
                u.Remember = true;
            else
                u.Remember = false;
            workUser.UpdateWhenIsRemember(u);
        }

        private void LoginRemembered()
        {
            User savedUser = workLogin.GetUserSaved();
            if (savedUser == null || savedUser.Remember)
            {
                txtPassword.Password = savedUser.Password;
                txtUsername.Text = savedUser.Username;
                remember.IsChecked = true;
                btnLogin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void LoginCine_Loaded(object sender, RoutedEventArgs e)
        {
            LoginRemembered();

        }

       

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Application.Current.Shutdown();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += delegate { DragMove(); };
        }
    }
}
