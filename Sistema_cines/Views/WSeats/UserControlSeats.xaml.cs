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

namespace Views.WSeats
{
    /// <summary>
    /// Lógica de interacción para UserControlSeats.xaml
    /// </summary>
    public partial class UserControlSeats : UserControl
    {
        List<Seat> seats = new List<Seat>();
        private int seatsAvailable;
        private int seatsSelected = 0;
        private int seatsNotAvailable;

        public UserControlSeats()
        {
            InitializeComponent();
        }

        private void DefaultSeatsConfiguration()
        {
            grid.Background = Brushes.WhiteSmoke;
            for (int row = 0; row < 7; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    Button btn = new Button();
                    btn.Name = "btn" + row.ToString() + column.ToString();
                    SetDefaultButtonProperty(btn);
                    Seat seat = new Seat();
                    seat.RowNumber = row;
                    seat.SeatNumber = column.ToString();
                    seats.Add(seat);
                    Grid.SetRow(btn, row);
                    Grid.SetColumn(btn, column);
                    grid.Children.Add(btn);
                    if (column == 2 && row == 1)
                    {
                        seat.Status = false;
                        SetButtonColor(btn, Brushes.Red);
                        seatsNotAvailable++;
                    }

                    seatsAvailable++;

                }
            }
            SetLabels();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DefaultSeatsConfiguration();
        }

        void ChangeButtonColor(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string row = btn.Name.Substring(3, 1);
            string column = btn.Name.Substring(4, 1);
            Seat seat = seats.SingleOrDefault(s => s.RowNumber == Convert.ToInt32(row) && s.SeatNumber == column);
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
