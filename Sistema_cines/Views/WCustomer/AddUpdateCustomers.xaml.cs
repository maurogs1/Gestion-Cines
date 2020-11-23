
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
using Views.UserControls;
using Views.Validation;
using Views.ViewModel;

namespace Views.WCustomer
{
    /// <summary>
    /// Lógica de interacción para AddUpdateCustomers.xaml
    /// </summary>
    public partial class AddUpdateCustomers : Window
    {
        WorkCustomer workCustomer;    // definicion la clase WorkCustomer para trabajar con base de datos 
        Customer c = new Customer();
        ListView listViewUpdate;
        bool isEdit;

        List<Customer> customers;

        public AddUpdateCustomers()
        {
            InitializeComponent();
            workCustomer = new WorkCustomer();
        }

        public AddUpdateCustomers(TypeWindow type)
        {
            InitializeComponent();
            workCustomer = new WorkCustomer();

            switch (type)
            {
                case TypeWindow.addCustomer:
                    btnAdd.Visibility = Visibility.Visible;
                    txtDni.Visibility = Visibility.Visible;

                    btnUpdate.Visibility = Visibility.Collapsed;
                    cmbCustomers.Visibility= Visibility.Collapsed;
                    break;

                case TypeWindow.updateCustomer:
                    ListBoxCustomers();
                    btnAdd.Visibility = Visibility.Collapsed;
                    txtDni.Visibility = Visibility.Collapsed;

                    btnUpdate.Visibility = Visibility.Visible;
                    cmbCustomers.Visibility = Visibility.Visible;

                    tittle.Text = "Actualizar datos del cliente";
                    break;

            }
        }


        public void ListBoxCustomers()
        {
            customers = workCustomer.GetAll();
            cmbCustomers.ItemsSource = workCustomer.GetAll();
        }

        private void elegirDni(object sender, RoutedEventArgs e) //cambia los campos
        {
            c = cmbCustomers.SelectedItem as Customer;

            txtLastname.Text = c.Lastname;
            txtName.Text = c.Name;
            txtPhone.Text = c.Phone;
            txtEmail.Text = c.Email;

        }

        public void SetModal(Customer m, bool edit, ListView listview)
        {
            listViewUpdate = listview;
            c = m;
            SetTxtItems(m, edit);
            DataContext = m;
            isEdit = edit;
        }

        private void SetTxtItems(Customer customer, bool edit)
        {
            txtDni.Text = customer.Dni;
            txtLastname.Text = customer.Lastname;
            txtName.Text = customer.Name;
            txtPhone.Text = customer.Phone;
            txtEmail.Text = customer.Email;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clear() //limpias los campos del formulario
        {
           
            txtDni.Clear();
            txtLastname.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }

        private void addCustomer(object sender, RoutedEventArgs e)
        {
            c = new Customer();            //Se carga los campos de un nuevo cliente
            c.Dni = txtDni.Text;
            c.Lastname = txtLastname.Text;
            c.Name = txtName.Text;
            c.Email = txtEmail.Text;
            c.Phone = txtPhone.Text;

            var result = MessagesBox.ShowDialog("¿Esta seguro de agregar cliente?", MessagesBox.Buttons.Yes_No);

            if (result == "1")
            {
                try
                {


                    if (workCustomer.AddCustom(c))
                    {
                        ShortNotifications.ShowDialog("Se agrego exitosamente!!!");
                        this.Close();
                        //clear();
                    }
                    else
                        DataContext = c;
                }
                catch (Exception)
                {
                    ShortNotifications.ShowDialog("Datos incorrectos!!!");
                }
            }
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            c.Lastname = txtLastname.Text;
            c.Name = txtName.Text;
            c.Email = txtEmail.Text;
            c.Phone = txtPhone.Text;

            var result = MessagesBox.ShowDialog("¿Esta seguro de modificar cliente?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                if (workCustomer.UpdateCustom(c))
                {
                    ShortNotifications.ShowDialog("Se modifico exitosamente!!!");
                    this.Close();
                }
                else
                {
                    DataContext = c;
                }
               
            }
        }


        private void Reset()
        {
            txtDni.Clear();
            txtLastname.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += delegate { DragMove(); };
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

    public enum TypeWindow
    {
        addCustomer, updateCustomer
    }
}
