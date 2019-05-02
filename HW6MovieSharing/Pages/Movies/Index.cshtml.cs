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
    /// Page for viewing summaries of all my movies
    /// </summary>
    public class IndexModel : BaseMoviePageModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The MovieContext</param>
        public IndexModel(HW6MovieSharing.Database.MovieContext context): base(context)
        {
        }

        /// <summary>
        /// My movies
        /// </summary>
        public IList<Movie> Movie { get; set; }

        /// <summary>
        /// All requests not from the current user
        /// </summary>
        public IList<MovieRequest> OtherMovieRequests { get; set; }

        /// <summary>
        /// Gets all movies belonging to me
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            Movie = await (from m in movieContext.Movie where m.Owner == AuthenticatedUserInfo.ObjectIdentifier select m).ToListAsync();
            OtherMovieRequests = await (from r in movieContext.Request where r.RequesterID != AuthenticatedUserInfo.ObjectIdentifier select r).ToListAsync();
        }
    }
}
