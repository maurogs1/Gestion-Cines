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
    /// Lógica de interacción para ModalConfiguration.xaml
    /// </summary>
    public partial class ModalConfiguration : Window

    {
        HallFactory hallFactory;
        WorkGroup workGroup;
        WorkSeat workSeat;
        int seatNumber = 0;
        int HallId;

        public ModalConfiguration()
        {
            InitializeComponent();
            workGroup = new WorkGroup();
            workSeat = new WorkSeat();
        }

        private void BtnAdd_Group(object sender, RoutedEventArgs e)
        {
            SPColumn.Visibility = Visibility.Visible;
            SPRow.Visibility = Visibility.Visible;
            SPName.Visibility = Visibility.Visible;
        }
        public void SetModal(HallFactory hf, int id)
        {
            hallFactory = hf;
            HallId = id;
            SPColumn.Visibility = Visibility.Hidden;
            SPRow.Visibility = Visibility.Hidden;
            SPName.Visibility = Visibility.Hidden;

        }

        private void BtnSave_Group(object sender, RoutedEventArgs e)
        {
            Group group = SetGroup();
            var result = MessagesBox.ShowDialog("¿Desea agregar grupo de butacas?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                SaveSeats(group);
                hallFactory.CreateHall();
                ShortNotifications.ShowDialog("Se agrego exitosamente!!!");
                this.Close();
            }
        }

        private Group SetGroup()
        {
            seatNumber=  SetSeatNumber();
            Group group = new Group();
            group.Totalcolumn = Convert.ToInt32(txtTotalColumn.Text);
            group.Totalrow = Convert.ToInt32(txtTotalRow.Text);
            group.GroupName = txtGroupName.Text;
            group.HallId = HallId;
            group.Id = workGroup.AddGroup(group);
            return group;
        }

        private int SetSeatNumber()
        {
            int number = 0;
            List<Group> list = workGroup.GetAllByHallId(HallId);
            for(int i = 0;i< list.Count; i++)
            {
                number += list[i].Totalcolumn * list[i].Totalrow;
            }
            return number+1;
        }

        private void SaveSeats(Group group)
        {

         
            

            List<Seat> seats = new List<Seat>();
           
            for(int i = 0; i< group.Totalrow; i++)
            {
                for(int j = 0; j < group.Totalcolumn; j++)
                {
                    Seat seat = new Seat();
                    seat.GroupId = group.Id;
                    seat.Rownumber = i;
                    seat.Columnnumber = j;
                    seat.Seatname = group.GroupName + j + i;
                    seat.Status = STATUS.DISPONIBLE.ToString();
                    seat.Seatname = seatNumber.ToString();
                    seats.Add(seat);
                    seatNumber++;
                }                
            }
            workSeat.AddAll(seats);
        }



        private void BtnCancel_Group(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SPColumn.Visibility = Visibility.Visible;
            SPRow.Visibility = Visibility.Visible;
            SPName.Visibility = Visibility.Visible;
        }
    }
}
