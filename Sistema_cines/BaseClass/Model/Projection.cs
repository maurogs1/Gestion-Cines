using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Projection
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Schedule { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }

        public Projection()
        {
          
        }
    }
}
