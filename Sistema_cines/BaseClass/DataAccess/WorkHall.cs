using BaseClass.IDataAccess;
using BaseClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
    public class WorkHall : IWorkGeneric<Hall>
    {



        public override void Add(Hall entity)
        {

            em.Hall.Add(entity);
            em.SaveChanges();
        }


        public override void Delete(int id)
        {
            Hall hall = em.Hall.Where(m => m.Id == id).First<Hall>();
            if (hall != null)
            {
                em.Hall.Remove(hall);
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Sala no encontrada");

        }

        public override List<Hall> GetAll()
        {

            return em.Hall.ToList<Hall>();
        }

        public override Hall GetEntity(int id)
        {
            return em.Hall.Where(m => m.Id == id).First<Hall>();
        }

        public override void Update(Hall entity)
        {
            Hall hall = em.Hall.Where(m => m.Id == entity.Id).First<Hall>();
            if (hall != null)
            {
                em.Entry(hall).CurrentValues.SetValues(entity);
                em.SaveChanges();
            }

        }
   
        
    
    }
}
