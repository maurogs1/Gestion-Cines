using BaseClass.IDataAccess;
using BaseClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
    public class WorkSeat: IWorkGeneric<Seat>
    {



        public static List<Seat> getSeats()
        {
            
            return null;
        }

        public override void Add(Seat entity)
        {
            em.Seat.Add(entity);
            em.SaveChanges();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Seat> GetAll()
        {
            return em.Seat.ToList();
        }

        public override Seat GetEntity(int id)
        {
            throw new NotImplementedException();
        }
        public Seat GetByRowAndColumn(int hallID, int groupId,int row, int column)
        {
            return  em.Seat.Where(g => g.GroupId == groupId && g.Rownumber == row && g.Columnnumber == column).First(); ;
            
        }
        public Seat GetByProjection(int projectionId, int groupId, int row, int column)
        {
            return em.Seat.Where(g => g.GroupId == groupId && g.Rownumber == row && g.Columnnumber == column && g.ProjectionId == projectionId).First(); ;

        }

        public override void Update(Seat entity)
        {
            Seat seat =  em.Seat.Find(entity.Id);
            if(seat != null)
            {
                em.Entry(seat).CurrentValues.SetValues(entity);
                em.SaveChanges();
            }
        }
        public void AddAll(List<Seat> entities)
        {
            em.Seat.AddRange(entities);
            em.SaveChanges();
        }
        public List<Seat> GetAllByGroupId(int groupId)
        {
            return  em.Seat.Where(g => g.GroupId == groupId).ToList();
        }

        public List<Seat> GetAllByProjectionAndGroup(int groupId, int projectionId)
        {
            return em.Seat.Where(g => g.GroupId == groupId && g.ProjectionId == projectionId).ToList();
        }

        public void DeleteAllByGroupId(int groupId)
        {
            em.Seat.RemoveRange(em.Seat.Where(g => g.GroupId == groupId).ToList());
        }

        public void UpdateAll(List<Seat> seats)
        {
            for (int i = 0; i < seats.Count; i++)
            {
                Update(seats[i]);
            }
        }

    }

  
    
}
