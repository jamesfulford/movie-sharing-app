using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW6MovieSharing.Database;
using HW6MovieSharing.Models;

namespace HW6MovieSharing.Pages.Movies
{
    /// <summary>
    /// Page for viewing details about a movie
    /// </summary>
    public class DetailsModel : BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext</param>
        public DetailsModel(HW6MovieSharing.Database.MovieContext context): base(context)
        {
        }

        /// <summary>
        /// The movie being viewed
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// Requests to borrow this movie
        /// </summary>
        public List<MovieRequest> Requests { get; set; }

        /// <summary>
        /// Gets movie to be viewed
        /// </summary>
        /// <param name="id">Id of movie to be reviewed</param>
        /// <returns>Page if movie with given id is found</returns>
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await movieContext.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }

            Requests = await (from r in movieContext.Request where r.MovieID == id select r).ToListAsync();
            return Page();
        }

        /// <summary>
        /// Acknowledge a movie has been returned successfully
        /// </summary>
        /// <param name="id">Id of movie to acknowledge return</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie movie = await movieContext.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }

            movie.SharedDate = null;
            movie.SharedWithEmailAddress = null;
            movie.SharedWithEmailAddress = null;
            movie.SharedWithFirstName = null;
            movie.SharedWithLastName = null;
            movie.SharedWithId = null;

            await movieContext.SaveChangesAsync();

            return Redirect($"./Details?id={movie.ID}");
        }
    }
}
