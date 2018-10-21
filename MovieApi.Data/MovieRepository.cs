using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace MovieApi.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext context;

        public MovieRepository(MovieDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return context.Movies
                .Include(m => m.Ratings);
        }

        public Movie GetMovie(int id)
        {
            return GetMovies().FirstOrDefault(m => m.MovieId == id);
        }

        public IEnumerable<Movie> GetRatedMoviesByUser(int userId)
        {
            return context.Ratings
                    .Where(r => r.UserId == userId)
                    .OrderByDescending(o => o.Rating)
                    .ThenBy(o => o.Movie.Title)
                .Select(r => r.Movie)
                .Include(m => m.Ratings);                
        }

        public IEnumerable<MovieRating> GetMovieRatings(int movieId) {
            return context.Ratings.Where(r => r.MovieId == movieId);
        }

        public MovieRating AddRating(MovieRating rating) {
            context.Ratings.Add(rating);            
            return rating;
        }

        public MovieRating UpdateRating(MovieRating rating) {
            context.Ratings.Update(rating);            
            return rating;
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }
       
    }
}
