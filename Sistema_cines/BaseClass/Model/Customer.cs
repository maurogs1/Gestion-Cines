using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Customer()
        {

        }

        public Customer(int id, string dni, string name, string lastname, string phone, string email)
        {
            this.Id = id;
            this.Dni = dni;
            this.Name = name;
            this.LastName = lastname;
            this.Phone = phone;
            this.Email = email;

        }
    }
}
