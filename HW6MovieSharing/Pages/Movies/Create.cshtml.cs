using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HW6MovieSharing.Database;
using HW6MovieSharing.Models;

namespace HW6MovieSharing.Pages.Movies
{
    /// <summary>
    /// Model for adding movies the user owns
    /// </summary>
    public class CreateModel : BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext</param>
        public CreateModel(HW6MovieSharing.Database.MovieContext context): base(context)
        {
        }

        /// <summary>
        /// Triggered by page load
        /// </summary>
        /// <returns>The page</returns>
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// The movie to show
        /// </summary>
        [BindProperty]
        public Movie Movie { get; set; }

        /// <summary>
        /// Triggered by submission of form
        /// </summary>
        /// <returns>Redirection to My Movies if valid</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.Owner = AuthenticatedUserInfo.ObjectIdentifier;

            movieContext.Movie.Add(Movie);
            await movieContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}