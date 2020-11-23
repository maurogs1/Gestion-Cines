using BaseClass;
using BaseClass.DataAccess;
using BaseClass.ModelDto;
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

namespace Views.WTicket
{
    /// <summary>
    /// Lógica de interacción para PrinterTicketView.xaml
    /// </summary>
    public partial class PrinterTicketView : Window
    {
        WorkCustomer wcustomer;
        WorkProjection wprojection;
        UserDto userDto;
        const string SELLER = "Vendedor: ";
        public PrinterTicketView(BaseClass.Ticket ticket)
        {
            InitializeComponent();
            wcustomer = new WorkCustomer();
            wprojection = new WorkProjection();
            userDto = UserDto.Instance;
        }

        public void SetTicketInfo(Ticket ticket, List<Seat> seats)
        {

            Customer customer = wcustomer.GetEntity(ticket.CustomerId);
            Projection projection = wprojection.GetEntity(ticket.ProjectionId);

            txtTicketNumero.Text = formatNumberTicket(ticket.Ticketnumber);
            txtTicketFecha.Text = ticket.Dateofsale.ToString();
            txtPrecioTotal.Text = "$" + ticket.Total.ToString();
            txtClienteNombre.Text = customer.Name;           
            txtProyeccionPelicula.Text = projection.Movie.Title;
            txtProyeccionFecha.Text = projection.Date.ToLongDateString();
            txtProyeccionHora.Text = projection.Date.ToLongTimeString();
            txtClienteDni.Text = customer.Dni;
            txtVendedorNombre.Text = SELLER + userDto.Fullname;
            SetSeatNumber(seats);
        }

        public void SetTicketInfo(Ticket ticket, Seat seat)
        {
            Customer customer = wcustomer.GetEntity(ticket.CustomerId);
            Projection projection = wprojection.GetEntity(ticket.ProjectionId);

            txtTicketNumero.Text = formatNumberTicket(ticket.Ticketnumber);
            txtTicketFecha.Text = ticket.Dateofsale.ToString();
            txtClienteNombre.Text = customer.Name;
            txtProyeccionPelicula.Text = projection.Movie.Title;
            txtProyeccionFecha.Text = projection.Date.ToLongDateString();
            txtProyeccionHora.Text = projection.Date.ToLongTimeString();
            txtClienteDni.Text = customer.Dni;
            txtVendedorNombre.Text = SELLER + userDto.Fullname;
            txtProyeccionUbicacion.Text = seat.Seatname;
        }

        private void SetSeatNumber(List<Seat> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if(i+1 == list.Count)
                {
                    txtProyeccionUbicacion.Text += list[i].Seatname + " ";
                }
                else
                txtProyeccionUbicacion.Text += list[i].Seatname + ", ";
            }
        }

        private string formatNumberTicket(int number)
        {
            string fmt = "00000000.##";
            return number.ToString(fmt);
        }
    }
}
