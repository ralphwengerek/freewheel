using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Model;

namespace MovieApi.Data
{
    public interface IMovieDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieRating> Ratings { get; set; }
        DbSet<User> Users { get; set; }
    }
}