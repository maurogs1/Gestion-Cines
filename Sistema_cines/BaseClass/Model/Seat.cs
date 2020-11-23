using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Seat
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }

        public Seat()
        {
           Status = true;


      
        }

        public Seat(int id, int rowNumber, int seatNumber, int groupId=1)
        {
            Id = id;
            RowNumber = rowNumber;
            ColumnNumber = seatNumber;
            Status = true;
            GroupId = groupId;
        }
    }
}
