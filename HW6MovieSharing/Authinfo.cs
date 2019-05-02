using System.Security.Claims;

namespace HW6MovieSharing
{
    /// <summary>
    /// Helper for extracting authenticated user info
    /// </summary>
    public static class ClaimsExtensions
    {
        /// <summary>
        /// Extracts the first name
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>The first name</returns>
        public static string FirstName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
        }
        /// <summary>
        /// Extracts the last name.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>The last name</returns>
        public static string LastName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;
        }

        /// <summary>
        /// Extracts the email address
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>The email address</returns>
        public static string EmailAddress(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? string.Empty;
        }

        /// <summary>
        /// Extracts the object's identifier.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>An identifier for the object</returns>
        public static string ObjectIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? string.Empty;
        }

    }

    /// <summary>
    /// Provides access to claims values
    /// </summary>
    public class DecodedClaims
    {
        /// <summary>
        /// The claims principal (the accessor helper)
        /// </summary>
        private readonly ClaimsPrincipal _claimsPrincipal;

        public DecodedClaims(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName
        {
            get
            {
                return _claimsPrincipal.FirstName();
            }
        }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName
        {
            get
            {
                return _claimsPrincipal.LastName();
            }
        }


        /// <summary>
        /// The email address.
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return _claimsPrincipal.EmailAddress();
            }
        }

        /// <summary>
        /// An identifier for the object
        /// </summary>
        public string ObjectIdentifier
        {
            get
            {
                return _claimsPrincipal.ObjectIdentifier();
            }
        }

    }
}
