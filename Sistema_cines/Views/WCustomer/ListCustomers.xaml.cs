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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views.ViewModel;
using Views.WCustomer;

namespace Views.UserControls
{
    /// <summary>
    /// Lógica de interacción para CRUD.xaml
    /// </summary>
    public partial class CRUDCustomer : UserControl
    {
        WorkCustomer wc = new WorkCustomer();
        WorkMovie wm = new WorkMovie();

        public CRUDCustomer()
        {
            InitializeComponent();
            SetDefaultCustomers();
        }


        private void SetDefaultCustomers()  //carga en la lista los cientes
        {
            this.List.ItemsSource = wc.GetAll();
            this.btnAdd.Visibility = Visibility.Visible;
        }

        

        private void BtnAdd_Click(object sender, RoutedEventArgs e) //opcion agregar nuevo cliente
        {
            AddUpdateCustomers addUpdate = new AddUpdateCustomers(TypeWindow.addCustomer);
            addUpdate.ShowDialog();
           
            CollectionViewSource.GetDefaultView(List.ItemsSource).Refresh();//refresca la lista de clientes
            SetDefaultCustomers();

        }


        private void ListBoxItemUpdateC_Selected(object sender, RoutedEventArgs e) //opcion actualizar un cliente
        {
            AddUpdateCustomers addUpdate = new AddUpdateCustomers(TypeWindow.updateCustomer);
            var baseobj = sender as FrameworkElement;
            Customer c = baseobj.DataContext as Customer;
            addUpdate.SetModal(c, true, List);
            addUpdate.ShowDialog();


            CollectionViewSource.GetDefaultView(List.ItemsSource).Refresh();//refresca la lista de clientes
            SetDefaultCustomers();

        }


        private void ListBoxItemDeleteC_Selected(object sender, RoutedEventArgs e) //opcion eliminar un cliente
        {
            var baseobj = sender as FrameworkElement;
            Customer customer = baseobj.DataContext as Customer;
            var result = MessagesBox.ShowDialog("¿Estás seguro de eliminar?", MessagesBox.Buttons.Yes_No);

            if (result == "1")
            {
                wc.Delete(customer.Id);
                CollectionViewSource.GetDefaultView(List.ItemsSource).Refresh();
                SetDefaultCustomers();
                ShortNotifications.ShowDialog("Se elimino exitosamente!!!");

            }
        }

        private void ListBoxItemUpdateM_Selected(object sender, RoutedEventArgs e) //opcion actualizar un cliente
        {
            var result = MessageBox.Show("¿Estás seguro de eliminar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Proceso de actualizacion en desarrollo");
            }

        }


        private void ListBoxItemDeleteM_Selected(object sender, RoutedEventArgs e) //opcion eliminar un cliente
        {

            var result = MessageBox.Show("¿Estás seguro de eliminar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Proceso de eliminacion en desarrollo");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateCustomers addUpdate = new AddUpdateCustomers(TypeWindow.updateCustomer);
            addUpdate.ShowDialog();

            CollectionViewSource.GetDefaultView(List.ItemsSource).Refresh();//refresca la lista de clientes
            SetDefaultCustomers();
        }
    }

 
}
