using BaseClass;
using BaseClass.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using Views.WCategory;

namespace Views.WMovie
{
    /// <summary>
    /// Lógica de interacción para ModalMovie.xaml
    /// </summary>
    public partial class ModalMovieDE : Window
    {
        DataGrid dataGridUpdate;
        Movie movie = new Movie();
        WorkMovie wm = new WorkMovie();
        List<Category> categories;
        WorkCategory wc = new WorkCategory();    
        bool isEdit;

        public string SourceUri
        {
            get
            {
                return System.IO.Path.GetFullPath("../../resources/img/not-available.jpg");
            }
        }

        public ModalMovieDE()
        {
            InitializeComponent();
            ListBoxCategory();
            
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
            cmbCategory.Text = "";
            cmbClassMovie.Text = "";
        }



        public void SetModal(Movie m,bool edit, DataGrid datagrid)
        {
            dataGridUpdate = datagrid;
            movie = m;
            SetTxtItems(m, edit);
            isEdit = edit;
        }

        

        private void SetTxtItems(Movie m, bool edit)
        {
            if (edit)
            {
                cmbClassMovie.SelectedValue = m.Classofmovie; 
                txtDuracion.Text = m.Duration;
                txtTitulo.Text = m.Title;
                cmbCategory.SelectedValue = wc.GetEntity(m.CategoryId).Name;
                ImageMovie.Source = LoadImage(m.Image);
            }
            if(m.Image == null)
               ImageMovie.Source =  new BitmapImage(new Uri(SourceUri));
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //ModalCategory modal = new ModalCategory(this);
            //modal.Show();
        }

        private void BtnAceptMovie_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("¿Estas seguro ?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                movie.Title = txtTitulo.Text;
                movie.Duration = txtDuracion.Text;
                movie.Classofmovie = cmbClassMovie.SelectedValue.ToString(); 
                Category asd = cmbCategory.SelectedItem as Category;
                movie.CategoryId = asd.Id;
                if (!isEdit)
                {
                    wm.Add(movie);
                    MessageBox.Show("Se agregó con éxito");
                }
                else
                {
                    wm.Update(movie);
                    MessageBox.Show("Se actualizó con éxito");

                }

            }
            Reset();
            dataGridUpdate.ItemsSource = wm.GetAll();
            CollectionViewSource.GetDefaultView(dataGridUpdate.ItemsSource).Refresh();
            this.Hide();
        }

        internal void Delete(int id)
        {
            wm.Delete(id);
            this.Hide();
        }


        private void BtnCancelMovie_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            this.Hide();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
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

        

        /// <summary>
        /// Cambia de byte[] (que tiene la foto) a BitmapImage, para usarlo dentro del resource de un atributo image
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

      

    }
}
