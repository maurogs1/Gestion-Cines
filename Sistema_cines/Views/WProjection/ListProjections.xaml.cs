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

namespace Views.WProjection
{
    /// <summary>
    /// Lógica de interacción para ListProjections.xaml
    /// </summary>
    public partial class ListProjections : UserControl
    {
        WorkProjection wk = new WorkProjection();
        ModalProjection modal = new ModalProjection();

        int id = 1;
        public ListProjections()
        {
            InitializeComponent();
          //  dgProjections.ItemsSource = wk.getAllProjections();

        }



        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //    CollectionViewSource.GetDefaultView(dgProjections.ItemsSource).Refresh();

            }
            catch
            {

            }


        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Projection projection = new Projection();
            projection.Id = id;
            id++;
            modal.SetProjection(projection, false, dgProjections);

            modal.Show();


        }
        private void Update(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Projection projection = baseobj.DataContext as Projection;
            modal.SetProjection(projection, true, dgProjections);
            modal.Show();
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Projection projection = baseobj.DataContext as Projection;
            var result = MessageBox.Show("¿Estás seguro de borrar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
         //     modal.DeleteProjection(projection.Id);
                CollectionViewSource.GetDefaultView(dgProjections.ItemsSource).Refresh();
            }
        }
        private void ListBoxItemUpdate_Selected(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Movie movie = baseobj.DataContext as Movie;
          //  modal.SetModal(movie, true, dgMovies);

            modal.Show();
        }

        private void ListBoxItemDelete_Selected(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Movie movie = baseobj.DataContext as Movie;
            var result = MessageBox.Show("¿Estás seguro de borrar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
              //  modal.Delete(movie.Id);
                MessageBox.Show("Borrado correctamente");
            //    dgMovies.ItemsSource = wm.GetAll();
           //     CollectionViewSource.GetDefaultView(dgMovies.ItemsSource).Refresh();
            }
        }
    }
}
