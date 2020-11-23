using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Rol()
        {

        }

        public Rol(int id, string description)
        {
            this.Id = id;
            this.Description = description;
        }
       
    }
}
