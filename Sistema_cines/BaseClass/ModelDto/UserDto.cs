using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.ModelDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        private readonly static UserDto _instance = new UserDto();

        private UserDto()
        {
        }

        public static UserDto Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
