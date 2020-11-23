using BaseClass;
using BaseClass.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para ModalMovie.xaml
    /// </summary>
    public partial class ModalMovie : UserControl
    {
        
        Movie movie = new Movie();
        WorkMovie wm = new WorkMovie();
        List<Category> categories;
        WorkCategory wc = new WorkCategory();

        //ItemsControl icUpdate;

        public static bool descending = false;

        public string SourceUri
        {
            get
            {
                return System.IO.Path.GetFullPath("../../resources/img/not-available.jpg");
            }
        } 

        public ModalMovie()
        {
            InitializeComponent();
            ListBoxCategory();
            Reset();
        }

        public void ListBoxCategory()
        {
            categories = wc.GetAll();
            cmbCategory.ItemsSource = wc.GetAll();
        }

        private void Reset()
        {
            ImageMovie.Source = null;
            txtTitulo.Clear();
            txtDuracion.Clear();
            txtDescripcion.Clear();
            txtUrlYoutube.Clear();
            txtUrlVideo.Text = "Seleccionar video...";
            cmbCategory.Text = "";
            cmbClassMovie.Text = "";
        }

        //add
        private void BtnAceptMovie_Click(object sender, RoutedEventArgs e)
        {
            var result = MessagesBox.ShowDialog("¿Estas seguro de agregar pelicula?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                movie.Title = txtTitulo.Text;
                movie.Duration = txtDuracion.Text;
                movie.Description = txtDescripcion.Text;
                movie.UrlYoutube = txtUrlYoutube.Text;
                movie.Classofmovie = cmbClassMovie.SelectedValue.ToString();
                Category asd = cmbCategory.SelectedItem as Category;
                movie.CategoryId = asd.Id;
                wm.Add(movie);
                ListCardMovies.listMovie.Insert(0, movie);
                ListCardMovies.listMovieFirstPage.Insert(0, movie);
                ShortNotifications.ShowDialog("Se agrego exitosamente!!!");
                movie = new Movie();
                descending = true;
            }
            Reset();
        }




        /// <summary>
        /// Abre el explorador para agregar una imagen, con extenciones JPG únicamente.
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param>
        private void BtnImage_Click(object sender, RoutedEventArgs e)
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
                ImageMovie.Source = bitmap;
                movie.Image = ConvertImageToBit(bitmap);
            }
        }

        /// <summary>
        /// Cambia la imagen a un array de byte, para guardarlo dentro del atributo image de peliculas
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
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

        private void BtnImage_Clicked(object sender, MouseButtonEventArgs e)
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
                ImageMovie.Source = bitmap;
                movie.Image = ConvertImageToBit(bitmap);
            }
        }

        private void BtnImage_Clicked(object sender, RoutedEventArgs e)
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
                ImageMovie.Source = bitmap;
                movie.Image = ConvertImageToBit(bitmap);
            }
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Video files (*.mp4)|*.mp4|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                movie.UrlTrailer = selectedFileName;
                txtUrlVideo.Text = "Video: " + selectedFileName;
            }
        }
         //
    }
}
