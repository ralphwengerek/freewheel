using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Data.Model
{
    public class Movie 
    {
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public int RunningTime { get; set; }

        public ICollection<MovieRating> Ratings { get; set; }
    }
}