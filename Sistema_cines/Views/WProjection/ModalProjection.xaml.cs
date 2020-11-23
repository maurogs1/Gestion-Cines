using BaseClass.DataAccess;
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
using Views.ViewModel;

namespace Views.WProjection
{
    /// <summary>
    /// Lógica de interacción para ModalProjection.xaml
    /// </summary>
    public partial class ModalProjection : Window
    {
        WorkProjection wk = new WorkProjection();
        WorkMovie mv = new WorkMovie(); 
        WorkHall wh = new WorkHall();
        Projection proj = new Projection();
        WorkGroup wg = new WorkGroup();
        WorkSeat ws = new WorkSeat();
        DataGrid dataGridUpdate;
        bool isEdit = false;
        User userLogged;

        public ModalProjection()
        {
            InitializeComponent();
            SetListBox();
        }
        public ModalProjection(User user)
        {
            InitializeComponent();
            SetListBox();
            userLogged = user;
        }

        private void SaveSeats(Group group, int seatNumber, int projectionId)
        {

            List<Seat> seats = new List<Seat>();
            
         
            for (int i = 0; i < group.Totalrow; i++)
            {
                for (int j = 0; j < group.Totalcolumn; j++)
                {
                    Seat seat = new Seat();
                    seat.GroupId = group.Id;
                    seat.Rownumber = i;
                    seat.Columnnumber = j;
                    seat.Seatname = group.GroupName + j + i;
                  Seat isAvailable =    ws.GetByRowAndColumn(0, group.Id, i, j);
                        if(isAvailable.Status == STATUS.FUERA_SERVICIO.ToString())
                    seat.Status = STATUS.FUERA_SERVICIO.ToString();
                        else
                    seat.Status = STATUS.DISPONIBLE.ToString();
                    seat.Seatname = seatNumber.ToString();
                    seat.ProjectionId = projectionId;
                    seats.Add(seat);
                    seatNumber++;
                }
            }
            ws.AddAll(seats);
        }

        private void GiveProjection(int hallId, int projectionId)
        {
            int seatNumber = 1;
            List<Group> groups = wg.GetAllByHallId(hallId);
            
            for (int i = 0; i< groups.Count; i++)
            {
                    SaveSeats(groups[i],seatNumber, projectionId);
                seatNumber++;
            }
        }

        private void BtnSave(object sender, RoutedEventArgs e)
        {
            var result = MessagesBox.ShowDialog("¿Esta seguro de agregar proyeccion?", MessagesBox.Buttons.Yes_No);


            if (result == "1")
            {
                proj.Date = (DateTime)dtpDate.Value;
                proj.MovieId = Convert.ToInt32(cmbMovie.SelectedValue);
                proj.HallId = Convert.ToInt32(cmbHall.SelectedValue);
                if (!isEdit)
                {
                    int projectionId =  wk.AddProjection(proj);
                    GiveProjection(proj.HallId, projectionId);
                    Projections.viewProjections.Add(proj);
                    ShortNotifications.ShowDialog("Se añadio exitosamente!!!");
                }
                else
                {
                    MessageBox.Show("Se actualizó con éxito");
                    wk.Update(proj);
                }
                Reset();
              //  dataGridUpdate.ItemsSource = wk.GetAll();
               // CollectionViewSource.GetDefaultView(dataGridUpdate.ItemsSource).Refresh();                
                this.Close();
            }
                
        }
    

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            Reset();
            this.Close();
        }
        private void Reset()
        {
            dtpDate.Value = null;
            cmbMovie.SelectedItem = null;
            cmbHall.SelectedValuePath = null;
            isEdit = false;
        }
        private void SetListBox()
        {

        cmbHall.ItemsSource = wh.GetAll();
         cmbMovie.ItemsSource = mv.GetAll();

        }
        private void SetUpdateProjection(Projection projection)
        {
            dtpDate.Value = projection.Date;
            cmbMovie.SelectedItem = projection.MovieId;
            //cmbHall.SelectedItem = projection.HallId;
        }

        public void SetProjection(Projection projection, bool edit, DataGrid datagrid= null)
        {
            dataGridUpdate = datagrid;         
            proj = projection;
            isEdit = edit;
        }

    }
}
