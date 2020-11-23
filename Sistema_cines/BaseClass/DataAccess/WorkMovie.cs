using BaseClass.IDataAccess;
using BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{
    

    public class WorkMovie: IWorkGeneric<Movie>
    {


        public WorkMovie()
        {
        
        }


        public override void Add(Movie entity)
        {
            em.Movie.Add(entity);
            em.SaveChanges();            
        }

        public override void Update(Movie entity)
        {
            Movie movie = em.Movie.Where(m => m.Id == entity.Id).First<Movie>();
            if (movie != null)
            {
                em.Entry(movie).CurrentValues.SetValues(entity);
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Película no encontrada");
        }

        public override void Delete(int id)
        {
            Movie movie = em.Movie.Where(m => m.Id == id).First<Movie>();
            if(movie != null)
            {
                em.Movie.Remove(movie);
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Película no encontrada");
        }

        public override List<Movie> GetAll()
        {
            return em.Movie.ToList<Movie>();
        }

        public ObservableCollection<Movie> GetAllMovies()
        {
            ObservableCollection<Movie> m = new ObservableCollection<Movie>(GetAll());
            return m;
        }

        
        public override Movie GetEntity(int id)
        {
            return em.Movie.Where(m => m.Id == id).First<Movie>();
        }
    }

    
}
