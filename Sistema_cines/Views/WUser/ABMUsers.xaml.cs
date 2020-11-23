using BaseClass.DataAccess;
using BaseClass;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Views.ViewModel;
using Views.Validation;

namespace Views.WUser
{
    /// <summary>
    /// Lógica de interacción para ABMUsers.xaml
    /// </summary>
    public partial class ABMUsers : UserControl
    {
        WorkUser wu = new WorkUser();
        User user;
        bool agregando = false;
        ObservableCollection<User> users;
        List<Rol> roles;
        CollectionView Vista;
        User currentUser;


        public ABMUsers()
        {
            InitializeComponent();
            users = wu.GetAllUsers();
            stpUsers.DataContext = users;
            loadRoles();
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(stpUsers.DataContext);
            currentUser = new User();
            copyCurrentUser();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            agregando = true;
            user = new User();
            users.Add(user);
            Vista.MoveCurrentToLast();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessagesBox.ShowDialog("¿Esta seguro de eliminar usuario: " + currentUser.Username + "?", MessagesBox.Buttons.Yes_No);

            if (result == "1")
            {
                EnableButtons();
                wu.Delete(users.ElementAt(Vista.CurrentPosition).Id);
                users.RemoveAt(Vista.CurrentPosition);
                ShortNotifications.ShowDialog("Se elimino exitosamente!!!");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (agregando)
            {
                users.RemoveAt(Vista.CurrentPosition);
                EnableButtons();
                agregando = false;
            }
            else
            {
                EnableButtons();
                restoreUser();
            }

        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (agregando)
            {
                var result = MessagesBox.ShowDialog("¿Esta seguro de agregar usuario: " + currentUser.Username + "?", MessagesBox.Buttons.Yes_No);

                if (result == "1")
                {
                    user.Fullname = txtFullname.Text;
                    user.Username = txtUsername.Text;
                    user.Password = txtPassword.Text;
                    Rol rol = cmbRol.SelectedItem as Rol;
                    user.RolId = rol.Id;
                    wu.Add(user);
                    EnableButtons();
                    ShortNotifications.ShowDialog("Se agrego exitosamente!!!");
                    agregando = false;

                    
                }
            }
            else
            {
                var result = MessagesBox.ShowDialog("¿Esta seguro de modificar el usuario: " + currentUser.Username + "?", MessagesBox.Buttons.Yes_No);

                if (result == "1")
                {
                    User userModified = new User();
                    userModified.Id = users.ElementAt(Vista.CurrentPosition).Id;
                    userModified.Fullname = txtFullname.Text;
                    userModified.Username = txtUsername.Text;
                    userModified.Password = txtPassword.Text;
                    Rol rol = cmbRol.SelectedItem as Rol;
                    userModified.Rol = rol;
                    userModified.RolId = userModified.Rol.Id;
                    wu.Update(userModified);
                    EnableButtons();
                    ShortNotifications.ShowDialog("Se modifico exitosamente!!!");
                    copyCurrentUser();
                }
            }
        }

        private void BtnPrimero_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
            copyCurrentUser();
        }

        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
            }
            copyCurrentUser();
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
            }
            copyCurrentUser();
        }

        private void BtnUltimo_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
            copyCurrentUser();
        }

        private void loadRoles()
        {
            WorkRol wr = new WorkRol();
            roles = wr.GetAll();
            cmbRol.ItemsSource = wr.GetAll();
        }

        private void ClearControls()
        {
            txtFullname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            agregando = false;
        }

        private void TxtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentUser.Username != txtUsername.Text)
            {
                DisableButtons();
            }
        }

        private void TxtFullname_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentUser.Fullname != txtFullname.Text)
            {
                DisableButtons();
            }
        }

        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentUser.Password != txtPassword.Text)
            {
                DisableButtons();
            }
        }

        private void CmbRol_DropDownClosed(object sender, EventArgs e)
        {
            DisableButtons();
        }

        private void copyCurrentUser()
        {
            currentUser.Fullname = users.ElementAt(Vista.CurrentPosition).Fullname;
            currentUser.Username = users.ElementAt(Vista.CurrentPosition).Username;
            currentUser.Password = users.ElementAt(Vista.CurrentPosition).Password;
            currentUser.Rol = users.ElementAt(Vista.CurrentPosition).Rol;
            currentUser.RolId = users.ElementAt(Vista.CurrentPosition).RolId;
            cmbRol.SelectedItem = currentUser.Rol;
        }

        private void restoreUser()
        {
            users.ElementAt(Vista.CurrentPosition).Fullname = currentUser.Fullname;
            users.ElementAt(Vista.CurrentPosition).Username = currentUser.Username;
            users.ElementAt(Vista.CurrentPosition).Password = currentUser.Password;
            users.ElementAt(Vista.CurrentPosition).Rol = currentUser.Rol;
            users.ElementAt(Vista.CurrentPosition).RolId = currentUser.RolId;
            cmbRol.SelectedIndex = currentUser.RolId - 1;
        }

        private void EnableButtons()
        {
            btnNuevo.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnPrimero.IsEnabled = true;
            btnAnterior.IsEnabled = true;
            btnSiguiente.IsEnabled = true;
            btnUltimo.IsEnabled = true;
        }

        private void DisableButtons()
        {
            btnNuevo.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnCancelar.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            btnPrimero.IsEnabled = false;
            btnAnterior.IsEnabled = false;
            btnSiguiente.IsEnabled = false;
            btnUltimo.IsEnabled = false;
        }

        private void BtnFilterUser(object sender, RoutedEventArgs e)
        {
            ListUser listUsers = new ListUser();
            listUsers.Show();
        }

        private void NoLetters_KeyDown(object sender, KeyEventArgs e)
        {
            Validations.NoLetters_KeyDown(e);
        }

        private void NoNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            Validations.NoNumbers_KeyDown(e);
        }
    }
}
