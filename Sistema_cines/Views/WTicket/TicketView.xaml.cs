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

namespace Views.WTicket
{
    /// <summary>
    /// Lógica de interacción para Ticket.xaml
    /// </summary>
    public partial class TicketView : Window
    {
        WorkCustomer wcustomer;
        WorkTicket wticket;
        WorkSeat wseat;
        Projection projection;
        decimal totalPrice;
        List<Seat> seats;

        public TicketView()
        {
            InitializeComponent();
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            wcustomer = new WorkCustomer();
            wticket = new WorkTicket();
            projection = new Projection();
            wseat = new WorkSeat();
            seats = new List<Seat>();
           
            cmbCustomer.ItemsSource = wcustomer.GetAll();
        }

        public void setConfiguration(Projection proj, List<Seat> list)
        {
            if (list.Count > 0) {
                txtProjectionId.Text = proj.Movie.Title;
                projection = proj;
                seats = list;
                SetSeatNumber(seats);
                CalculatePrice(list);
                txtTotal.Content = totalPrice;
            }
            else
            {
                BtnImage.IsEnabled = false;
            }
        }

        public void CalculatePrice(List<Seat> list)
        {
            totalPrice = 0;
            for(int i = 0; i < list.Count; i++)
            {
                totalPrice = totalPrice + 250;
            }
        }

        private void SetSeatNumber(List<Seat> list)
        {
            for(int i = 0; i< list.Count; i++)
            {              
                if (i + 1 == list.Count)
                {
                    txtTotalTickets.Text += list[i].Seatname + " ";
                }
                else
                    txtTotalTickets.Text += list[i].Seatname + ", ";
            }
        }

 
        private void UpdateSeats(List<Seat> list)
        {
            for(int i = 0; i< list.Count; i++)
            {
                wseat.Update(list[i]);
            }
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.ProjectionId = projection.Id;
            ticket.Ticketnumber = generateNumberTicket();
            ticket.CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            ticket.Dateofsale = DateTime.Now;
            ticket.Total = totalPrice;
            var result = MessagesBox.ShowDialog("¿Desea generar Ticket?", MessagesBox.Buttons.Yes_No);
            if (result == "1")
            {
                UpdateSeats(seats);
                wticket.Add(ticket);
                ShortNotifications.ShowDialog("Se genero ticket!!!");
                this.Close();
                PrintTicket(ticket, seats);
            }
        }

        private void PrintTicket(Ticket ticket, List<Seat> seats)
        {
            PrinterTicketView printerTicketView = new PrinterTicketView(ticket);
            printerTicketView.Show();
            printerTicketView.SetTicketInfo(ticket,seats);
        }

        private void PrintTickets(Ticket ticket, List<Seat> seats)
        {

        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private int generateNumberTicket()
        {
            return wticket.getCountTickets() + 1;
        }

    }
}
