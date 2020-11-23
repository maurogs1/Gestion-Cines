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

namespace Views.Document
{
    /// <summary>
    /// Lógica de interacción para DocumentFilterUser.xaml
    /// </summary>
    public partial class DocumentFilterUser : Window
    {
        public DocumentFilterUser(ListView list)
        {
            InitializeComponent();
            List.ItemsSource = list.ItemsSource;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)DocUsers).DocumentPaginator,"Imprimir");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
