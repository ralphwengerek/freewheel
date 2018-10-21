using MovieApi.Data;
using MovieApi.Data.Model;
using MovieApi.Models;
using System;
using System.Linq;

namespace MovieApi.Controllers
{

    public class ModelFactory
    {
        IMovieRepository repo;

        public ModelFactory(IMovieRepository repo)
        {
            this.repo = repo;
        }

        public MovieModel Create(Movie movie) {
            return new MovieModel() {
                Id = movie.MovieId,
                Title = movie.Title,
                RunningTime = movie.RunningTime,
                YearOfRelease = movie.YearOfRelease,
                AverageRating = Math.Round(movie.Ratings.Average(a => a.Rating) * 2) / 2
            };
        }

        public MovieRatingModel Create(MovieRating rating) {
            return new MovieRatingModel()
            {
                Movie = repo.GetMovie(rating.MovieId).Title,
                Rating = rating.Rating
            };
        }
    }
}