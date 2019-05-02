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
    /// Model for page to delete a movie the user owns
    /// </summary>
    public class DeleteModel : BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext</param>
        public DeleteModel(HW6MovieSharing.Database.MovieContext context): base(context)
        {
        }

        /// <summary>
        /// The movie to be deleted
        /// </summary>
        [BindProperty]
        public Movie Movie { get; set; }

        /// <summary>
        /// Gets details about movie to be deleted
        /// </summary>
        /// <param name="id">Id of movie to be deleted</param>
        /// <returns>The page if movie with given id can be found</returns>
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
            return Page();
        }

        /// <summary>
        /// Deletes movie
        /// </summary>
        /// <param name="id">Id of movie to be deleted</param>
        /// <returns>A redirect to index page if id is provided</returns>
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await movieContext.Movie.FindAsync(id);

            if (Movie != null)
            {
                movieContext.Movie.Remove(Movie);
                await movieContext.SaveChangesAsync();
            }

            movieContext.Request.RemoveRange(
                await (from r in movieContext.Request where r.MovieID == id select r).ToListAsync()
            );
            await movieContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
