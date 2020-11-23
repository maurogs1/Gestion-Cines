using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
   public class Hall
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
            public Hall()
        {

        }

        public Hall(int id, int capacity)
        {
            Id = id;
            Capacity = capacity;
        }
    }
}
