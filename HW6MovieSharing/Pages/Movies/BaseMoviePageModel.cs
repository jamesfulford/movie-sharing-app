using HW6MovieSharing.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW6MovieSharing.Pages.Movies
{
    /// <summary>
    /// Base class movie pages can inherit from to access db context and authinfo
    /// </summary>
    public class BaseMoviePageModel : PageModel
    {

        /// <summary>
        /// The db MovieContext
        /// </summary>
        protected readonly MovieContext movieContext;

        /// <summary>
        /// The decoded claims
        /// </summary>
        private DecodedClaims _decodedClaims = null;

        /// <summary>
        /// Gets the authenticated user information.
        /// </summary>
        /// <value>The authenticated user information.</value>
        public DecodedClaims AuthenticatedUserInfo
        {
            get
            {
                if (_decodedClaims == null)
                {
                    _decodedClaims = new DecodedClaims(this.User);
                }
                return _decodedClaims;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMoviePageModel"/> class.
        /// </summary>
        public BaseMoviePageModel(HW6MovieSharing.Database.MovieContext context)
        {
            movieContext = context;
        }

    }
}
