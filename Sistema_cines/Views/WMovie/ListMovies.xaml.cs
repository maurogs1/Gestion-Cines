using BaseClass;
using BaseClass.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Views.WMovie
{
    /// <summary>
    /// Lógica de interacción para ListMovies.xaml
    /// </summary>
    public partial class ListMovies : UserControl
    {
        ModalMovie modal = new ModalMovie();

        WorkMovie wm = new WorkMovie();

        

        public ListMovies()
        {
            InitializeComponent();
            SetDefault();
        }

        private void SetDefault()
        {
            var asd = wm.GetAll(); ;

            dgMovies.ItemsSource = wm.GetAll();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //modal.SetModal(new Movie(), false, dgMovies);
            //modal.Show();
        }

        private void ListBoxItemUpdate_Selected(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Movie movie = baseobj.DataContext as Movie;
            //modal.SetModal(movie, true, dgMovies);
           
            //modal.Show();
        }

        private void ListBoxItemDelete_Selected(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Movie movie = baseobj.DataContext as Movie;
            var result = MessageBox.Show("¿Estás seguro de borrar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                //modal.Delete(movie.Id);
                MessageBox.Show("Borrado correctamente");
                dgMovies.ItemsSource = wm.GetAll();
                CollectionViewSource.GetDefaultView(dgMovies.ItemsSource).Refresh();
            }
        }

        public void asd()
        {
            
        }

    }
}
