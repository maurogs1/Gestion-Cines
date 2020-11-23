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
using System.Windows.Shapes;

namespace Views.WHall
{
    /// <summary>
    /// Lógica de interacción para ModalGroup.xaml
    /// </summary>
    public partial class ModalGroup : Window
    {
        public ModalGroup()
        {
            InitializeComponent();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Group(object sender, RoutedEventArgs e)
        {
            Group group = new Group();
            group.Totalcolumn = Convert.ToInt32(txtTotalColumn.Text);
            group.Totalrow = Convert.ToInt32(txtTotalRow.Text);
        }

        private void BtnCancel_Group(object sender, RoutedEventArgs e)
        {

        }
    }
}
