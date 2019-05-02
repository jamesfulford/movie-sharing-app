using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HW6MovieSharing.Models;

namespace HW6MovieSharing.Database
{
    /// <summary>
    /// Database context for Movies
    /// </summary>
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options): base(options) {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Access to all Movies
        /// </summary>
        public DbSet<Movie> Movie { get; set; }

        /// <summary>
        /// Access to all movie borrow Requests
        /// </summary>
        public DbSet<MovieRequest> Request { get; set; }

        /// <summary>
        /// Connects Model to Table
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Links Todos entity model to "Todos" table.
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<MovieRequest>().ToTable("Request");
        }
    }
}
