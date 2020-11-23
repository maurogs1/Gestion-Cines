using BaseClass.Model;
using System;
using BaseClass.IDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
    public class WorkProjection: IWorkGeneric<Projection>
    {

       

        public override void Add(Projection entity)
        {
            em.Projection.Add(entity);
            em.SaveChanges();
        }

        public int AddProjection(Projection entity)
        {
            em.Projection.Add(entity);
            em.SaveChanges();
            return entity.Id;

        }



        public override void Delete(int id)
        {
            Projection projection = em.Projection.Find(id);
            if (projection != null)
            {
                em.Projection.Remove(projection);
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Projección no encontrada");
        }

       
       

        public override List<Projection> GetAll()
        {
            return em.Projection.ToList<Projection>();
        }

       

        public override Projection GetEntity(int id)
        {
            return em.Projection.Where(m => m.Id == id).First<Projection>();
        }

        public override void Update(Projection entity)
        {
            Projection projection = em.Projection.Where(m => m.Id == entity.Id).First<Projection>();
            if (projection != null)
            {
                em.Entry(projection).CurrentValues.SetValues(entity);
                em.SaveChanges();
            }
        }
    }
}
