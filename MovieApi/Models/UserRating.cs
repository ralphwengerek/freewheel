using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models
{
    public class UserRating
    {
        public int UserId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}