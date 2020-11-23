using BaseClass.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
    public class WorkRol : IWorkGeneric<Rol>
    {
        public WorkRol()
        {

        }

        public override void Add(Rol entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Rol> GetAll()
        {
            return em.Rol.ToList<Rol>();
        }

        public override Rol GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Rol entity)
        {
            throw new NotImplementedException();
        }
    }
}
