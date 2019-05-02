using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HW6MovieSharing.Models
{
    /// <summary>
    /// Represents a borrow request made of a movie by a user.
    /// </summary>
    public class MovieRequest
    {
        /// <summary>
        /// Unique identifier for each request
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequestID { get; set; }

        /// <summary>
        /// ID of Movie requester is asking to borrow
        /// </summary>
        public long MovieID { get; set; }

        /// <summary>
        /// ID of user who requested item
        /// </summary>
        public string RequesterID { get; set; }

        /// <summary>
        /// Requester's email address
        /// </summary>
        public string RequesterEmailAddress { get; set; }

        /// <summary>
        /// Requester's last name
        /// </summary>
        public string RequesterLastName { get; set; }

        /// <summary>
        /// Requester's first name
        /// </summary>
        public string RequesterFirstName { get; set; }

        /// <summary>
        /// Timestamp of when request was made
        /// </summary>
        public DateTime TimeRequested { get; set; }
    }
}
