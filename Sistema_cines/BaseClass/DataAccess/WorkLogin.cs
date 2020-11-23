using BaseClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClass.IDataAccess;
using System.Threading.Tasks;
namespace BaseClass.DataAccess
{
    public class WorkLogin : IWorkGeneric<User>
    {
        private WorkUser workUser;
        private User userLogged;

        public WorkLogin()
        {
            workUser = new WorkUser();
        }
        
        public bool LoginUser(string userName, string password)
        {
            if(workUser.searchUserByUsernameAndPassword(userName, password)!=null)
            {
                userLogged = workUser.searchUserByUsernameAndPassword(userName, password);
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool LoginUser(string userName, string password, bool remember)
        {
            if (workUser.searchUserByUsernameAndPassword(userName, password) != null)
            {
                userLogged = workUser.searchUserByUsernameAndPassword(userName, password);
                return true;
            }
            else
            {
                return false;
            }

        }

        public User GetUserSaved()
        {
            try
            {
                return em.User.Where(m => m.Remember == true).First<User>();
            }
            catch { return new User(); }
            
        }




        public User getUserLogged()
        {
            return this.userLogged;
        }

        public override void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public override User GetEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
