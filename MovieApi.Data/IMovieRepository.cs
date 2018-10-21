using System.Collections.Generic;
using System.Linq;
using MovieApi.Data.Model;

namespace MovieApi.Data
{
    public interface IMovieRepository 
    {
        Movie GetMovie(int id);
        IEnumerable<Movie> GetMovies();
        IEnumerable<Movie> GetRatedMoviesByUser(int userId);
        IEnumerable<MovieRating> GetMovieRatings(int movieId);
        MovieRating AddRating(MovieRating rating);
        MovieRating UpdateRating(MovieRating rating);
        bool SaveAll();
    }
}