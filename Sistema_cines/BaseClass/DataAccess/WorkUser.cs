using BaseClass.IDataAccess;
using BaseClass.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
    public class WorkUser: IWorkGeneric<User>
    {
        public WorkUser()
        {
        }

        public User searchUserByUsernameAndPassword(string username, string password)
        {
            User userAux = new User();
            userAux.Username = username;
            userAux.Password = password;
            try
            {
                User user = em.User.Where(m => m.Username.Equals(userAux.Username)  && m.Password.Equals(userAux.Password)).First<User>();
                return user;
        }
            catch
            {
                return null;
            }
               
            
        }

        public override void Add(User entity)
        {
            em.User.Add(entity);
            em.SaveChanges();
        }

        public override void Update(User entity)
        {
            

            User user = em.User.Find(entity.Id);
            if(user != null)
            {
                user.RolId = entity.RolId;
                user.Password = entity.Password;
                user.Fullname = entity.Fullname;
                user.Username = entity.Username;
                user.Remember = entity.Remember;
                em.Entry(user.Rol).State = EntityState.Unchanged;
                em.SaveChanges();
            }else
                throw new ArgumentException("Usuario no encontrado");



        }

        public  void UpdateWhenIsRemember(User entity)
        {
            em.User.SqlQuery("UPDATE 'User' set remember = 0");

            User user = em.User.Find(entity.Id);
            if (user != null)
            {
                user.RolId = entity.RolId;
                user.Password = entity.Password;
                user.Fullname = entity.Fullname;
                user.Username = entity.Username;
                user.Remember = entity.Remember;
                em.Entry(user.Rol).State = EntityState.Unchanged;
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Usuario no encontrado");

        }

 


        public override void Delete(int id)
        {
            User user = em.User.Where(u => u.Id == id).First<User>();
            if (user != null)
            {
                em.User.Remove(user);
                em.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Usuario no encontrado");
            }
        }

        public override List<User> GetAll()
        {
            return em.User.ToList<User>();
        }

        public ObservableCollection<User> GetAllUsers()
        {
            ObservableCollection<User> users= new ObservableCollection<User>(GetAll());
            return users;
        }
        

        public override User GetEntity(int id)
        {
            return em.User.Where(u => u.Id == id).First<User>();
        }

    }
}
