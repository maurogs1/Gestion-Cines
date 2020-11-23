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

namespace Views.WMovie
{
    /// <summary>
    /// Lógica de interacción para Movies.xaml
    /// </summary>
    public partial class Movies : Window
    {
        
        ModalMovie modal = new ModalMovie();
    
        WorkMovie wm = new WorkMovie();

        

        public Movies()
        {
            InitializeComponent();

            var movies = wm.GetAll();

            if (movies.Count > 0)
            {
                ListViewMovies.ItemsSource = movies;
            }
        }

        

        

        //private void BtnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    var baseobj = sender as FrameworkElement;
        //    Movie movie = baseobj.DataContext as Movie;
        //    var result = MessageBox.Show("¿Estás seguro de borrar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

        //    if (result == MessageBoxResult.Yes)
        //    {
        //        modal.Delete(movie.Id);
        //        MessageBox.Show("Borrado correctamente");
        //        //dgMovies.ItemsSource = wm.GetAll();
        //        //CollectionViewSource.GetDefaultView(dgMovies.ItemsSource).Refresh();
        //    }
        //}

        

        private void closeModalMovies_Click(object sender, RoutedEventArgs e)
        //private void closeModalMovies()
        {
            ModalMovie m = stpModalMovie.Children.OfType<ModalMovie>().FirstOrDefault(x => x.Name == "ModalMoviesUC");
            if (m != null)
            {
                stpModalMovie.Children.Remove(m);
            }
        }


    }
}
