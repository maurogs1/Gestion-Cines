using BaseClass.Model;
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

namespace Views
{
    /// <summary>
    /// Lógica de interacción para Seats.xaml
    /// </summary>
    public partial class Seats : UserControl
    {

        List<Seat> seats;
        private int seatsAvailable = 0;
        private int seatsSelected = 0;
        private int seatsNotAvailable = 0;

        public Seats()
        {

            InitializeComponent();
            seats = new List<Seat>();
            DefaultSeatsConfiguration();
        }

        private void DefaultSeatsConfiguration()
        {

            seatsAvailable = 0;
            seatsSelected = 0;
            seatsNotAvailable = 0;
            SeatsConfiguration(grid2, 7, 7);
            SeatsConfiguration(grid, 7, 4);
            SetLabels();
        }

        void SeatsConfiguration(Grid gridName, int totalRow, int totalColumn)
        {
            for (int row = 0; row < totalRow; row++)
            {
                for (int column = 0; column < totalColumn; column++)
                {
                    Button btn = new Button();

                    btn.Name = "btn" + row.ToString() + column.ToString();
                    SetDefaultButtonProperty(btn);
                    Seat seat = new Seat();
                    seat.RowNumber = row;
                    seat.ColumnNumber = column;
                    seats.Add(seat);
                    Grid.SetRow(btn, row);
                    Grid.SetColumn(btn, column);
                    gridName.Children.Add(btn);
                    if (column == 2 && row == 1)
                    {
                        seat.Status = false;
                        SetButtonColor(btn, Brushes.Red);
                        seatsNotAvailable++;
                    }
                    seatsAvailable++;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        void ChangeButtonColor(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string row = btn.Name.Substring(3, 1);
            string column = btn.Name.Substring(4, 1);

            Seat seat = seats.First(s => s.RowNumber == Convert.ToInt32(row) && s.ColumnNumber == Convert.ToInt32(column) );
            if (seat.Status && btn.Background == Brushes.Green)
            {
                SetButtonColor(btn, Brushes.Gray);
                seatsAvailable++;
                seatsSelected--;
            }
            else if (seat.Status && btn.Background == Brushes.Gray)
            {
                SetButtonColor(btn, Brushes.Green);
                seatsAvailable--;
                seatsSelected++;
            }
            else
                MessageBox.Show("Está ocupado");
            SetLabels();
        }

        private void SetButtonColor(Button btn, Brush Color)
        {

            btn.Content = new PackIcon { Kind = PackIconKind.Seat, Background = Color };
            btn.Background = Color;
        }
        private void SetDefaultButtonProperty(Button btn)
        {

            btn.Click += ChangeButtonColor;
            btn.Content = new PackIcon { Kind = PackIconKind.Seat, Background = Brushes.DarkGray };
            btn.Background = Brushes.Gray;
            btn.BorderBrush = null;
            btn.Padding = new Thickness(10);
            btn.Width = 35;
            btn.Height = 35;
        }
        private void SetLabels()
        {
            lblAvailable.Content = seatsAvailable - seatsNotAvailable;
            lblSelected.Content = seatsSelected;
            lblNotAvailable.Content = seatsNotAvailable;
        }
    }
}
