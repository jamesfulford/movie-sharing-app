using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW6MovieSharing.Database;
using HW6MovieSharing.Models;

namespace HW6MovieSharing.Pages.Movies
{
    /// <summary>
    /// Page model for editing a movie
    /// </summary>
    public class EditModel : BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext</param>
        public EditModel(HW6MovieSharing.Database.MovieContext context): base(context)
        {
        }

        /// <summary>
        /// The movie to edit
        /// </summary>
        [BindProperty]
        public Movie Movie { get; set; }

        /// <summary>
        /// Sets details about movie to edit
        /// </summary>
        /// <param name="id">Id of movie to edit</param>
        /// <returns>Edit page if id provided corresponds to a movie</returns>
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
        /// Edits movie
        /// </summary>
        /// <returns>A redirect to index if movie is successfully edited</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.Owner = AuthenticatedUserInfo.ObjectIdentifier;
            movieContext.Attach(Movie).State = EntityState.Modified;

            try
            {
                await movieContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(long id)
        {
            return movieContext.Movie.Any(e => e.ID == id);
        }
    }
}
