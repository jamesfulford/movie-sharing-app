using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW6MovieSharing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HW6MovieSharing.Pages.Movies.Requests
{
    /// <summary>
    /// Model for accepting a movie request
    /// </summary>
    public class AcceptModel : BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext</param>
        public AcceptModel(HW6MovieSharing.Database.MovieContext context) : base(context)
        {
        }

        /// <summary>
        /// The movie request to be considered for acceptance
        /// </summary>
        [BindProperty]
        public MovieRequest MovieRequest { get; set; }

        /// <summary>
        /// The movie under consideration
        /// </summary>
        [BindProperty]
        public Movie Movie { get; set; }

        /// <summary>
        /// Gets details about request to be accepted
        /// </summary>
        /// <param name="id">Id of request to be considered</param>
        /// <returns>The page if request with given id can be found</returns>
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MovieRequest = await movieContext.Request.FirstOrDefaultAsync(r => r.RequestID == id);

            if (MovieRequest == null)
            {
                return NotFound();
            }

            Movie = await movieContext.Movie.FirstOrDefaultAsync(m => m.ID == MovieRequest.MovieID);

            if (Movie == null)
            {
                // MovieRequest is invalid
                movieContext.Request.Remove(MovieRequest);
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// Accept a movie request
        /// </summary>
        /// <param name="id">Request id</param>
        /// <returns>Redirect to details if successful</returns>
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            MovieRequest req = await movieContext.Request.FirstOrDefaultAsync(r => r.RequestID == id);

            if (req == null)
            {
                return NotFound();
            }

            Movie movie = await movieContext.Movie.FirstOrDefaultAsync(m => m.ID == req.MovieID);

            if (movie == null)
            {
                return NotFound();
            }

            // Cannot do this operation because of 'race condition'
            if (!movie.Shareable || movie.SharedWithId != null)
            {
                return BadRequest("Movie cannot be shared");
            }

            // Mark movie as shared
            movie.SharedDate = DateTime.Now;
            movie.SharedWithId = req.RequesterID;
            movie.SharedWithEmailAddress = req.RequesterEmailAddress;
            movie.SharedWithFirstName = req.RequesterFirstName;
            movie.SharedWithLastName = req.RequesterLastName;

            // Remove request
            movieContext.Request.Remove(req);

            await movieContext.SaveChangesAsync();

            return Redirect($"../Details?id={movie.ID}");
        }
    }
}