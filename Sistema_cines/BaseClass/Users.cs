using BaseClass.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass
{
    public class Users : ObservableCollection<User>
    {
        public Users()
        {
            WorkUser wu = new WorkUser();
            foreach(User u in wu.GetAll())
            {
                Add(u);
            }
        }
    }
}
