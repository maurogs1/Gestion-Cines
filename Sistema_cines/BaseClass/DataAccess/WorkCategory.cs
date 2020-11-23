using BaseClass.IDataAccess;
using BaseClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaseClass.DataAccess
{

    public class WorkCategory: IWorkGeneric<Category>
    {
        private static List<Category> categories = new List<Category>();

   

        public WorkCategory()
        {
      

        }   


        public static List<Category> getCategories()
        {
         

            return categories;
        }

        public override void Add(Category entity)
        {
            em.Category.Add(entity);
            em.SaveChanges();
        }

        public override void Delete(int id)
        {

            Category category = em.Category.Where(c => c.Id == id).First<Category>();
            if (category != null)
            {
                em.Category.Remove(category);
                em.SaveChanges();
            }
            
        }

        public override List<Category> GetAll()
        {

            return em.Category.ToList<Category>();
        }

        public override Category GetEntity(int id)
        {
            return em.Category.Where(c => c.Id == id).First<Category>();
            
        }

        public override void Update(Category entity)
        {
            Category category = em.Category.Where(c => c.Id == entity.Id).First<Category>();
            if (category != null)
            {
                category = entity;
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Categoria no encontrada");
               
            
        }
    }
}
