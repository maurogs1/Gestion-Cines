using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Group
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int HallId { get; set; }
        public Group()
        {
        }

        public Group(int row, int column, int hallId)
        {
            Row = row;
            Column = column;
            HallId = hallId;
        }
    }
}
