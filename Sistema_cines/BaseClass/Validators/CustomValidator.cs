using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Validators
{
    public class CustomValidator
    {

        public bool CustomerValidator(Customer customer)
        {

            return customer.Dni == null && customer.Email == null && customer.Lastname == null && customer.Name == null && customer.Phone == null;

        }
    }
}
