using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HW6MovieSharing.Models
{
    public class Movie
    {
        /// <summary>
        /// Unique identifier for each movie
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        /// <summary>
        /// Title of movie
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Generic genre of movie
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// ID of owner of movie in identity provider.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// String to display representing the owner
        /// </summary>
        public string OwnerDisplay { get; set; }

        /// <summary>
        /// Whether the movie is publicly listed
        /// </summary>
        public bool Shareable { get; set; }

        /// <summary>
        /// Whether this movie is being returned right now (implies is Shareable and also has the SharedWith fields filled
        /// </summary>
        public bool BeingReturned { get; set; }

        //
        // Section: who movie is shared with (if applicable)
        //

        /// <summary>
        /// First name of person shared with
        /// </summary>
        public string SharedWithFirstName { get; set; }

        /// <summary>
        /// Last name of person shared with
        /// </summary>
        public string SharedWithLastName { get; set; }

        /// <summary>
        /// Email address of person shared with
        /// </summary>
        public string SharedWithEmailAddress { get; set; }

        /// <summary>
        /// Id of person shared with
        /// </summary>
        public string SharedWithId { get; set; }

        /// <summary>
        /// Datetime when movie was shared
        /// </summary>
        public DateTime? SharedDate { get; set; }
    }
}
