using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int RolId { get; set; }

        public User()
        {

        }

        public User(int id, string username, string password, string fullname, int rolId)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.FullName = fullname; 
            this.RolId = rolId;

        }
    }
}
