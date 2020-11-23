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
using Views.ViewModel;

namespace Views.WHall
{
    /// <summary>
    /// Lógica de interacción para ModalHall.xaml
    /// </summary>
    public partial class ModalHall : Window
    {
        WorkHall wh = new WorkHall();
        HallFactory hallFactory;
        public ModalHall()
        {
            InitializeComponent();
        }

       

        private void BtnCancel_Group(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Group(object sender, RoutedEventArgs e)
        {
            Hall hall = new Hall();
            hall.Capacity = Convert.ToInt32(txtCapacity.Text);
            hall.HallNumber = Convert.ToInt32(txtHallNumber.Text);
            var result = MessagesBox.ShowDialog("¿Esta seguro de agregar sala?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                wh.Add(hall);
                hallFactory.RefreshCombobox();
                hallFactory.CreateHall();
                ShortNotifications.ShowDialog("Se añadio exitosamente!!!");
                this.Close();
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            SPGroups.IsEnabled = true;
        }
        public void SetModal(HallFactory hf)
        {
            hallFactory = hf;
        }
    }
}
