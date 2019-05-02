using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW6MovieSharing.Pages
{
    /// <summary>
    /// Error page
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// The RequestId which failed
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Whether to show RequestId or no
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Gets RequestId
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
