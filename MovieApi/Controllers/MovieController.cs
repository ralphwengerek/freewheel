using Microsoft.AspNetCore.Mvc;
using MovieApi.Data;
using MovieApi.Data.Model;
using System;
using System.Linq;
using MovieApi.Models;
using Microsoft.Extensions.Logging;
using MovieApi.ActionFilters;
using System.Collections.Generic;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ApiBase
    {

        public MovieController(IMovieRepository repo, ILogger<MovieController> logger) : base(repo, logger)
        {
        }

        // api/movies?filter={filter}&term={term}
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<MovieModel>> GetMoviesByFilter(string filter, string term)
        {
            IEnumerable<Movie> baseQuery;

            switch (filter)
            {
                case "title":
                    baseQuery = Repository.GetMovies().Where(m => m.Title.ToLower().Contains(term.ToLower()));
                    break;
                case "genre":
                    baseQuery = Repository.GetMovies().Where(m => m.Genre.ToLower().Contains(term.ToLower()));
                    break;
                case "year":
                    {
                        var year = 0;
                        int.TryParse(term, out year);
                        if (year > 1900 && year <= DateTime.Now.Year)
                        {
                            baseQuery = Repository.GetMovies().Where(m => m.YearOfRelease == year);
                        }
                        else
                        {
                            return BadRequest("Invalid year");
                        }
                    }
                    break;
                default: return BadRequest("Invalid filter");
            }

            if (baseQuery.Count() > 0)
            {
                return Ok(baseQuery.Select(m => ModelFactory.Create(m)));
            }
            else
            {
                return NotFound();
            }
        }

        // api/movies/top
        [HttpGet("top")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<MovieModel>> GetTopRatedMovies()
        {
            return Ok(Repository.GetMovies()
                .Select(m => ModelFactory.Create(m))
                .ToList()
                .OrderByDescending(o => o.AverageRating)
                .ThenBy(o => o.Title)
                .Take(5));
        }

        // api/movies/top/{userId}
        [HttpGet("top/{userId}")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<MovieModel>> GetTopRatedMoviesForUser(int userId)
        {
            return Ok(Repository.GetRatedMoviesByUser(userId)
                .Select(m => ModelFactory.Create(m))
                .Take(5));
        }


        // Add or update a user rating for a movie       
        [HttpPut("{movieId}/rating")]
        [HttpPatch("{movieId}/rating")]
        [ValidateModel]
        public ActionResult<MovieRatingModel> AddOrUpdateRating(int movieId, [FromBody] UserRating rating)
        {
            try
            {
                var movieRating = Repository.GetMovieRatings(movieId).Where(r => r.UserId == rating.UserId).FirstOrDefault();

                if (movieRating == null)
                {
                    try
                    {
                        MovieRating m = Repository.AddRating(new MovieRating { MovieId = movieId, UserId = rating.UserId, Rating = rating.Rating });
                        return CreatedAtAction("GetMovieRating", new { movieId= movieId, userId = movieRating.UserId }, ModelFactory.Create(m));
                    }
                    catch
                    {
                        return BadRequest("No such movie");
                    }
                }
                else
                {
                    movieRating.Rating = rating.Rating;
                    var result = ModelFactory.Create(Repository.UpdateRating(movieRating));
                    Repository.SaveAll();
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"AddOrUpdateRating: {ex}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{movieId}/rating")]
        public ActionResult<MovieRatingModel> GetMovieRating(int movieId, int userId) {

            var rating = Repository.GetMovieRatings(movieId).Where(r => r.UserId == userId).FirstOrDefault();

            if (rating == null)
            {
                return NotFound();
            }
            else {
                return Ok(ModelFactory.Create(rating));
            }

        }
        
    }
}
