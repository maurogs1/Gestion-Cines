using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClass.IDataAccess;
using BaseClass.Model;
namespace BaseClass.DataAccess
{
 public  class WorkGroup: IWorkGeneric<Group>
    {
        static List<Group> getGroups()
        {
            //List<Seat> seats = WorkSeat.getSeats();



            List<Group> groups = new List<Group>();
            //groups.Add(new Group(7, 7, 1));
            //groups.Add(new Group(7, 7, 1));
            return groups;
        }

        public override void Add(Group entity)
        {
            em.Group.Add(entity);
            em.SaveChanges();
            int id = entity.Id;
        }
        public int AddGroup(Group entity)
        {
            em.Group.Add(entity);
            em.SaveChanges();
            return entity.Id;

        }
        public override void Delete(int id)
        {
            Group group = em.Group.Where(m => m.Id == id).First<Group>();
            if (group != null)
            {
                em.Group.Remove(group);
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Grupo no encontrado");

        }

        public override List<Group> GetAll()
        {
            return em.Group.ToList();
        }

        public override Group GetEntity(int id)
        {
            return em.Group.Where(m => m.Id == id).First<Group>();
        }

        public override void Update(Group entity)
        {
            Group group = em.Group.Where(m => m.Id == entity.Id).First<Group>();
            if(group != null)
            {
                em.Entry(group).CurrentValues.SetValues(entity);
                em.SaveChanges();
            }              
        }
        public List<Group> GetAllByHallId(int hallId)
        {
            return  em.Group.Where(g => g.HallId == hallId).ToList();
        }
    }
}
