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
using Views.WMovie;

namespace Views.WCategory
{
    /// <summary>
    /// Lógica de interacción para ModalCategory.xaml
    /// </summary>
    public partial class ModalCategory : Window
    {

        WorkCategory wc = new WorkCategory();
        private ModalMovie modalMovie;

        public ModalCategory()
        {
            InitializeComponent();
        }

        public ModalCategory(ModalMovie modalMovie)
        {
            InitializeComponent();

            this.modalMovie = modalMovie;
        }

        private void BtnSave(object sender, RoutedEventArgs e)
        {
            Category category = new Category();
            category.Name = txtTitulo.Text;
            wc.Add(category);
            modalMovie.ListBoxCategory();
            this.Close();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
