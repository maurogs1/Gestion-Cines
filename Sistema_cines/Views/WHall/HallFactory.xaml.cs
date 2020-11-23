using BaseClass;
using BaseClass.DataAccess;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views.Messages;
using Views.ViewModel;

namespace Views.WHall
{
    /// <summary>
    /// Lógica de interacción para HallFactory.xaml
    /// </summary>
    public partial class HallFactory : UserControl
    {
        
       WorkHall wh;
        WorkGroup wg;
        WorkSeat ws;
        Grid defaultGrid;
        int HallId;
        //List<Seat> seats;
        List<Group> groups;
        int gridsCreated = 0;
        List<Grid> grds;
        List<Seat> seatsUpdate;
        private int seatsAvailable = 0;
        private int seatsSelected = 0;
    // private int seatsNotAvailable = 0;
        public HallFactory()
        {
            InitializeComponent();
            wh = new WorkHall();
            wg = new WorkGroup();
            ws = new WorkSeat();
            groups = new List<Group>();
            grds = new List<Grid>();
            seatsUpdate = new List<Seat>();
            defaultGrid = grdContainer;
            RefreshCombobox();
        }
        
        public void RefreshCombobox()
        {
            cmbHall.ItemsSource = wh.GetAll();
            

        }






        public void CreateHall()
        {
            //obtengo todos los grupos que tiene la sala seleccionada
            groups = wg.GetAllByHallId(HallId);            
            gridsCreated = groups.Count();
            for (int i = 0;i< groups.Count();i++)
            {
                //obtengo todas las butacas que tiene cada grupo
                List<Seat> seats = ws.GetAllByGroupId(groups[i].Id);
                //al grid principal se le agrega una nueva columna por cada grupo que tenga la sala
                grdContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto});
                Grid grid = new Grid();
                grid.Name ="grd" + groups[i].Id+"z";
                Grid.SetRow(grid, i+1);
                Grid.SetColumn(grid ,i +1);
                SeatsConfiguration(grid, groups[i].Totalrow, groups[i].Totalcolumn, groups[i].Id);
                CreateGridDefinition(grid, groups[i].Totalrow, groups[i].Totalcolumn);
                grds.Add(grid);
                grdContainer.Children.Add(grid);

            }
        }

        private void RemoveGrids(int totalGrids)
        {
            if (gridsCreated > 0)
            {
               for(int i = 0; i< grds.Count; i++)
                {
                    grdContainer.Children.Remove(grds[i]);
                }
            }
            
      
            
                
            
        }


        private void CreateGridDefinition(Grid grid, int totalRow, int totalColumn)
        {
                
            for (int i = 0; i < totalRow; i++)
            {
                RowDefinition rw = new RowDefinition();
                rw.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rw);
            }
            for(int i = 0; i< totalColumn; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(cd);
            }
            grid.Margin= new Thickness(10);
        }

        void SeatsConfiguration(Grid gridName, int totalRow, int totalColumn, int groupId)
        {
            for (int row = 0; row < totalRow; row++)
            {
     
                for (int column = 0; column < totalColumn; column++)
                {
                    //Se usa esas letras para identificar cuando comienza y termina cada columna o fila
                    //por ejemplo para extraer el valor del row, comienza con la letra r y termina con la letra R
                    // para obtener la columna, comienza con la letra c y termina con la letra C
                    //las letras son arbitrarias
                    Seat seat = ws.GetByRowAndColumn(HallId, groupId, row, column);

                    Button btn = new Button();
                    btn.Name = "btnr" + row.ToString()+"R"+"c" +column.ToString()+"C";
                    SetDefaultButtonProperty(btn,seat.Status);                  
                    Grid.SetRow(btn, row);
                    Grid.SetRow(btn, row);
                    Grid.SetColumn(btn, column);
                    gridName.Children.Add(btn);
                }
            }
        }
        private void DefaultSeatsConfiguration()
        {

           
        
        }

        //para obtener una cadena a partir de una letra al principio y una letra al final
        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }


        void ChangeButtonColor(object sender, RoutedEventArgs e)
        {

            //Obtengo el botón al que clickeé
            Button btn = (sender as Button);
            //obtengo el nombre del grid que contiene al botón
            string gridName = (btn.Parent as Grid).Name;
            var idGroup = Convert.ToInt32(Between(gridName, "d", "z"));
            var row = Convert.ToInt32(Between(btn.Name, "r", "R"));
            int column = Convert.ToInt32(Between(btn.Name, "c", "C"));

            Seat seat = ws.GetByRowAndColumn(HallId, idGroup, row, column);

            // Seat seat = ws.GetByRowAndColumn(s => s.Rownumber == Convert.ToInt32(row) && s.Columnnumber == Convert.ToInt32(column));
            if ( btn.Background == Brushes.Yellow)
            {
                SetButtonColor(btn, Brushes.Gray);
                seat.Status = STATUS.DISPONIBLE.ToString();
                seatsAvailable++;
            }
            else if (btn.Background == Brushes.Gray)
            {
                SetButtonColor(btn, Brushes.Yellow);
                seat.Status = STATUS.FUERA_SERVICIO.ToString();
                seatsAvailable--;
             
            }
            seatsUpdate.Add(seat);
            SetLabels();
        }

        private void SetButtonColor(Button btn, Brush Color)
        {

            btn.Content = new PackIcon { Kind = PackIconKind.Seat, Background = Color };
            btn.Background = Color;
        }
        private void SetDefaultButtonProperty(Button btn, string isAvailable = "")
        {
            btn.Click += ChangeButtonColor;
            if (isAvailable == STATUS.FUERA_SERVICIO.ToString())
            {
                btn.Content = new PackIcon { Kind = PackIconKind.Seat, Background = Brushes.Yellow };
                btn.Background = Brushes.Yellow;

            }

            else
            {
                btn.Content = new PackIcon { Kind = PackIconKind.Seat, Background = Brushes.Gray };
                btn.Background = Brushes.Gray;

            }

            btn.BorderBrush = null;
            btn.Padding = new Thickness(10);
            btn.Width = 35;
            btn.Height = 35;
        }
        private void SetLabels()
        {
           
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ModalHall modal = new ModalHall();
            modal.SetModal(this);
            modal.Show();
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            ModalConfiguration modal = new ModalConfiguration();
            modal.SetModal(this,HallId);
            modal.Show();
        }

        private void CmbHall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hall hall = wh.GetEntity((cmbHall.SelectedItem as Hall).Id);
            lblAvailable.Content = hall.Capacity;
            HallId = hall.Id;
            RemoveGrids(gridsCreated);
            CreateHall();
            
            BtnConfiguration.IsEnabled = true;
        }

        private void BtnSeatConfiguration_Click(object sender, RoutedEventArgs e)
        {

            ws.UpdateAll(seatsUpdate);
            ShortNotifications.ShowDialog("Butacas actualizadas");
            CreateHall();
        }
    }
}
