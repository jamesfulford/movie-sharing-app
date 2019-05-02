using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW6MovieSharing.Database;
using HW6MovieSharing.Models;

namespace HW6MovieSharing.Pages
{
    /// <summary>
    /// Page showing all borrowable movies
    /// </summary>
    public class IndexModel : Movies.BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext></param>
        public IndexModel(MovieContext context): base(context)
        {
        }

        /// <summary>
        /// List of borrowable movies to show
        /// </summary>
        public IList<Movie> Movie { get; set; }

        /// <summary>
        /// All requests not from the current user
        /// </summary>
        public IList<MovieRequest> MyMovieRequests { get; set; }

        /// <summary>
        /// Gets all borrowable movies
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            Movie = await (
                from m in movieContext.Movie
                where m.Owner != AuthenticatedUserInfo.ObjectIdentifier  // Not me
                select m
            ).ToListAsync();
            MyMovieRequests = await (from r in movieContext.Request where r.RequesterID == AuthenticatedUserInfo.ObjectIdentifier select r).ToListAsync();
        }

        /// <summary>
        /// Initiate a movie's return
        /// </summary>
        /// <param name="id">ID of movie to return</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return BadRequest("Please provide an id url parameter");
            }
            Movie movie = await movieContext.Movie.FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            // If have already borrowed, attempt return
            if (movie.SharedWithId == AuthenticatedUserInfo.ObjectIdentifier)
            {
                // (if return request has already been made, no problem)
                movie.BeingReturned = true;
                await movieContext.SaveChangesAsync();
                return Redirect("./Index");
            }

            // We are requesting to borrow the movie

            // Check that movie is shareable
            if (!movie.Shareable)
            {
                return BadRequest("Cannot borrow an unshareable movie");
            }
        
            // Check for a prior request
            if ((from r in movieContext.Request where r.MovieID == id && r.RequesterID == AuthenticatedUserInfo.ObjectIdentifier select true).Any())
            {
                return BadRequest("You have already requested to borrow this movie.");
            }

            // Add request
            MovieRequest req = new MovieRequest()
            {
                MovieID = movie.ID,
                RequesterID = AuthenticatedUserInfo.ObjectIdentifier,
                RequesterEmailAddress = AuthenticatedUserInfo.EmailAddress,
                RequesterFirstName = AuthenticatedUserInfo.FirstName,
                RequesterLastName = AuthenticatedUserInfo.LastName,
                TimeRequested = DateTime.Now,
            };
            movieContext.Request.Add(req);
            await movieContext.SaveChangesAsync();
            return Redirect("./Index");
        }
    }
}
