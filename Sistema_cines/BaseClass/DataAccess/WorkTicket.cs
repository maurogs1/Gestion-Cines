using BaseClass.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
  public class WorkTicket : IWorkGeneric<Ticket>
    {
        public override void Add(Ticket entity)
        {
            em.Ticket.Add(entity);
            em.SaveChanges();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Ticket> GetAll()
        {
            List<Ticket> tickets = em.Ticket.ToList<Ticket>();
            return tickets;
        }

        public override Ticket GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public int getCountTickets()
        {
            return em.Ticket.Count<Ticket>();
        }
    }
}
