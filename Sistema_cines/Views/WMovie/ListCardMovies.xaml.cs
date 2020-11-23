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
    /// Lógica de interacción para ListCardMovies.xaml
    /// </summary>
    public partial class ListCardMovies : UserControl
    {
        ModalMovie modal = new ModalMovie();
        WorkMovie workMovie = new WorkMovie();
        public static ObservableCollection<Movie> listMovie;
        public static ObservableCollection<Movie> listMovieFirstPage;
        public static ObservableCollection<Movie> listMovieSecondPage;

        public ListCardMovies()
        {
            InitializeComponent();
            LoadMovies();
            LoadFirstPageMovies();
            listMovie = new ObservableCollection<Movie>(listMovie.Reverse());
            LoadFirstPageMovies();
            //listMovieFirstPage=new ObservableCollection<Movie>(listMovie.Reverse());
        }

        private void LoadMovies()
        {
            listMovie = new ObservableCollection<Movie>();
            listMovie = workMovie.GetAllMovies();
        }

        private void LoadFirstPageMovies()
        {
            listMovieFirstPage = new ObservableCollection<Movie>();
            for (int i = 0; i < 8 && i<listMovie.Count; i++)
            {
                listMovieFirstPage.Add(listMovie.ElementAt(i));
            }
            ListViewMovies.ItemsSource = listMovieFirstPage;
        }

        private void LoadSecondPageMovies()
        {
            listMovieSecondPage = new ObservableCollection<Movie>();
            for (int i = 8; i < 16 && i < listMovie.Count; i++)
            {
                listMovieSecondPage.Add(listMovie.ElementAt(i));
            }
            ListViewMovies.ItemsSource = listMovieSecondPage;
        }

        private void BtnSecondPage_Click(object sender, RoutedEventArgs e)
        {
            LoadSecondPageMovies();
        }

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            LoadFirstPageMovies();
        }

        private void BtnGridStyle_Click(object sender, RoutedEventArgs e)
        {
            ListViewMovies.ItemsSource = listMovie;
        }

        //private void CardMovie_MouseDown(object sender, MouseButtonEventArgs e) //prueba
        //{
        //    if (e.ChangedButton == MouseButton.Left)
        //    {
        //        MessageBox.Show("Funciona el click ..sig paso  !");
        //    }

        //    var baseobj = sender as FrameworkElement;
        //    Movie movie = baseobj.DataContext as Movie;
        //    var result = MessageBox.Show("¿Estás seguro de borrar?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

        //    if (result == MessageBoxResult.Yes)
        //    {
        //        workMovie.Delete(movie.Id);
        //        MessageBox.Show("Borrado correctamente");
        //    }

        //    //theMovies = workMovie.GetAllMovies();
        //    //CollectionViewSource.GetDefaultView(theMovies).Refresh();//refresca la lista

        //}
    }
}
