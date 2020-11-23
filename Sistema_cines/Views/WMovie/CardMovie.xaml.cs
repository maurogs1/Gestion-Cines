using BaseClass;
using BaseClass.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Views.WMovie
{
    /// <summary>
    /// Lógica de interacción para CardMovie.xaml
    /// </summary>
    public partial class CardMovie : UserControl
    {
        WorkMovie workMovie = new WorkMovie();
        Movie movieEdit;
        List<Category> categories;
        WorkCategory wc = new WorkCategory();

        public static ItemsControl icUpdate;

        public CardMovie()
        {
            InitializeComponent();
            ListBoxCategory();
            ReColorAddButtons();
        }

        public static void CardMovieRefresh(ItemsControl ic)
        {
            CardMovie.icUpdate = ic;
        }

        public void ListBoxCategory()
        {
            categories = wc.GetAll();
            cmbCategoria.ItemsSource = wc.GetAll();
        }

        //delete
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Movie movie = baseobj.DataContext as Movie;
            var result = MessagesBox.ShowDialog("¿Estás seguro de eliminar?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                try
                {
                    workMovie.Delete(movie.Id);
                    ShortNotifications.ShowDialog("Se elimino exitosamente!!!");
                    ListCardMovies.listMovie.Remove(movie);
                    CollectionViewSource.GetDefaultView(ListCardMovies.listMovie).Refresh();//refresca la lista
                }
                catch
                {
                    ShortNotifications.ShowDialog("No se puede eliminar pelicula, esta en proyeccion");
                }
            }
        }

        private void BtnTrailer_Click(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Movie movie = baseobj.DataContext as Movie;     
            VideoPlayer videoPlayer = new VideoPlayer(movie);
            videoPlayer.Show();
        }

        private void BtnMostrarEditar_Click(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            movieEdit = baseobj.DataContext as Movie;
        }

        private void BtnAceptarEditar_Click(object sender, RoutedEventArgs e)
        {
            movieEdit.Title = txtTitulo.Text;
            movieEdit.Description = txtDescripcion.Text;
            movieEdit.Duration = txtDuracion.Text;
            movieEdit.Classofmovie = cmbClase.SelectedValue.ToString();
            Category categoria = cmbCategoria.SelectedItem as Category;
            movieEdit.CategoryId = categoria.Id;
            workMovie.Update(movieEdit);
            movieEdit = new Movie();
            ReColorAddButtons();
        }

        private void BtnAddVideo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Video files (*.mp4)|*.mp4|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                movieEdit.UrlTrailer = selectedFileName;
                btnAddVideo.Background = Brushes.Green;
                btnAddVideo.BorderBrush = Brushes.Green;
            }
        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                movieEdit.Image = ConvertImageToBit(bitmap);
                btnAddImage.Background = Brushes.Green;
                btnAddImage.BorderBrush = Brushes.Green;
            }
        }

        private byte[] ConvertImageToBit(BitmapImage bitmap)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
                return data;
            }
        }

        private void ReColorAddButtons()
        {
            btnAddImage.Background = Brushes.DodgerBlue;
            btnAddImage.BorderBrush = Brushes.DodgerBlue;
            btnAddVideo.Background = Brushes.DodgerBlue;
            btnAddVideo.BorderBrush = Brushes.DodgerBlue;
        }
    }
}
