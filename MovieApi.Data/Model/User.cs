using System.Collections.Generic;

namespace MovieApi.Data.Model
{
    public class User 
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<MovieRating> RatedMovies { get; set; }
    }
}
