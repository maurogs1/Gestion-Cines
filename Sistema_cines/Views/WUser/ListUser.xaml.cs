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
using Views.Document;
using Views.ViewModel;

namespace Views.WUser
{
    /// <summary>
    /// Lógica de interacción para ListUser.xaml
    /// </summary>
    public partial class ListUser : Window
    {
        WorkUser workUser = new WorkUser();
        private CollectionViewSource collectionFilter;
        public ListUser()
        {
            InitializeComponent();
            collectionFilter = Resources["ViewUser"] as CollectionViewSource;
        }

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (collectionFilter != null)
            {
                collectionFilter.Filter += eventViewUserFilter;
            }
        }

        private void eventViewUserFilter(object sender, FilterEventArgs e)
        {
            User user = e.Item as User;
            if (user.Username.StartsWith(txtUsername.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            var result = MessagesBox.ShowDialog("¿Desea generar Documento?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                DocumentFilterUser documentFilterUser = new DocumentFilterUser(List);
                ShortNotifications.ShowDialog("Se genero documento con exito!!!");
                documentFilterUser.Show();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
