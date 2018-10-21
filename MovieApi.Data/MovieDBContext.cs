using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Model;
using System;
using System.Collections.Generic;

namespace MovieApi.Data
{
    public class MovieDbContext : DbContext, IMovieDbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieRating> Ratings { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }

    }
}
