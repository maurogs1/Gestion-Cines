using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public int CategoryId { get; set; }
        public string ClassOfMovie { get; set; }
        public string Image { get; set; }
        public Movie()
        {

        }

        public Movie(int id, string title, string duration, int gender, string classOfMovie)
        {
            this.Id = id; 
            this.Title = title;
            this.Duration =duration;
            this.CategoryId = gender;
            this.ClassOfMovie = classOfMovie;

        }

    }


}
