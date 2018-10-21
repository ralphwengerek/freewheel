using Microsoft.Extensions.DependencyInjection;
using MovieApi.Data;
using MovieApi.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApi
{
    public class Seeder
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<MovieDbContext>();

            if (!context.Users.Any())
            {
                var users = new List<User>();
                users.Add(new User { UserId = 1, FirstName = "John", LastName = "Doe" });
                users.Add(new User { UserId = 2, FirstName = "Mike", LastName = "Merry" });
                users.Add(new User { UserId = 3, FirstName = "Pete", LastName = "Pratt" });
                users.Add(new User { UserId = 4, FirstName = "Barry", LastName = "Bowman" });
                users.Add(new User { UserId = 5, FirstName = "Luke", LastName = "Letterman" });

                context.Users.AddRange(users);
                context.SaveChanges();

                var movies = new List<Movie>();
                movies.Add(new Movie { MovieId = 1, Genre = "Action", RunningTime = 108, Title = "Pulp Fiction", YearOfRelease = 1995 });
                movies.Add(new Movie { MovieId = 2, Genre = "Sci-fi", RunningTime = 90, Title = "The Matrix", YearOfRelease = 1999 });
                movies.Add(new Movie { MovieId = 3, Genre = "Drama", RunningTime = 88, Title = "The Godfather", YearOfRelease = 1972 });
                movies.Add(new Movie { MovieId = 4, Genre = "Action", RunningTime = 95, Title = "Mission Impossible", YearOfRelease = 1995 });
                movies.Add(new Movie { MovieId = 5, Genre = "Fantasy", RunningTime = 300, Title = "Lord of the Rings", YearOfRelease = 2005 });

                context.Movies.AddRange(movies);

                var ratings = new List<MovieRating>();
                Random rnd = new Random();

                foreach (var user in users)
                {
                    foreach (var movie in movies)
                    {
                        ratings.Add(new MovieRating { MovieId = movie.MovieId, Rating = rnd.Next(6), UserId = user.UserId });
                    }
                }

                context.Ratings.AddRange(ratings);
                context.SaveChanges();
            }
        }
    }
}
