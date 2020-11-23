using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Ticket
    {
        private int Id { get; set; }
        private string NumberTicket { get; set; }
        private DateTime DateOfSale { get; set; }
        private int CustomerId { get; set; }
        private int ProjectionId { get; set; }
        private string SeatNumber { get; set; }
        private string RoomNumber { get; set; }

        public Ticket()
        {

        }
    }
}
