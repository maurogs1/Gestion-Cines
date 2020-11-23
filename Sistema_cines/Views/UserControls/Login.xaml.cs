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
using BaseClass.DataAccess;
using BaseClass;
using Views.UserControls;

namespace Views.Login
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        WorkLogin workLogin;
        WorkUser workUser;
        User user;
        public Login()
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
                MessageBox.Show("Welcome " + workLogin.getUserLogged().Fullname, "Login successful");
            }
            else
            {
                MessageBox.Show("Wrong username or password", "Error");
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
        
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //Linea para cerrar el UC junto a la ventana
            //Window.GetWindow(this).Close();
            Application.Current.Shutdown();
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

   

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoginRemembered();

        }
    }
}
