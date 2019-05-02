using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW6MovieSharing.Models;

namespace HW6MovieSharing.Database
{
    /// <summary>
    /// Class to manage database seeding
    /// </summary>
    public class Seeds
    {
        /// <summary>
        /// Seeds database with sample movies (heavily biased to Sci Fi :) )
        /// </summary>
        /// <param name="context">MovieContext to seed</param>
        public static void Sow (MovieContext context)
        {
            if (context.Movie.Any())
            {
                return;
            }

            string MeID = "ba297747-58c3-4b7f-8202-8e6d95903278";

            Movie[] movies = new Movie[]
            {
                // My movies (from requirements)
                new Movie()
                {
                    Title = "Star Wars IV",
                    Category = "Science Fiction",
                    Shareable = true,
                    BeingReturned = false,
                    Owner = MeID,
                    OwnerDisplay = "Jar Jar Binks",
                },
                new Movie()
                {
                    Title = "The Matrix",
                    Category = "Science Fiction",
                    Shareable = true,
                    BeingReturned = false,
                    Owner = MeID,
                    OwnerDisplay = "James Fulford",
                },
                new Movie()
                {
                    Title = "The Social Network",
                    Category = "Drama",
                    Shareable = true,
                    BeingReturned = false,
                    Owner = MeID,
                    OwnerDisplay = "James Fulford",
                },
                new Movie()
                {
                    Title = "Star Trek First Contact",
                    Category = "Science Fiction",
                    Shareable = true,
                    BeingReturned = false,
                    Owner = MeID,
                    OwnerDisplay = "James Fulford",
                },
                new Movie()
                {
                    Title = "Captain Marvel",
                    Category = "Adventure",
                    Shareable = false,
                    BeingReturned = false,
                    Owner = MeID,
                    OwnerDisplay = "James Fulford",
                },
                // Test initiating a movie return
                new Movie()
                {
                    Title = "Star Wars I",
                    Category = "Comedy",
                    Shareable = true,
                    BeingReturned = false,
                    Owner = "47-58d3-4b7f-8202-8e6d95903278",
                    OwnerDisplay = "Jar Jar Binks",
                    SharedWithId = MeID,
                    SharedWithEmailAddress = "james.fulford.temporary@outlook.com",
                    SharedWithFirstName = "James",
                    SharedWithLastName = "Fulford",
                    SharedDate = DateTime.Now,
                },
                // Test acknowledging a movie return
                new Movie()
                {
                    Title = "Star Wars III",
                    Category = "Space Opera",
                    Shareable = true,
                    BeingReturned = true,
                    Owner = MeID,
                    OwnerDisplay = "Jar Jar Binks",
                    SharedWithId = "asdfasd",
                    SharedWithEmailAddress = "james.fulford.temporary@outlook.com",
                    SharedWithFirstName = "James",
                    SharedWithLastName = "Fulford",
                    SharedDate = DateTime.Now,
                },
                // Should not see this
                new Movie()
                {
                    Title = "Star Wars II",
                    Category = "Action",
                    Shareable = true,
                    BeingReturned = true,
                    Owner = "47-58d3-4b7f-8202-8e6d95903278",
                    OwnerDisplay = "Jar Jar Binks",
                    SharedWithId = "47-58d3-4b7f-8202-8e6d95903278",
                    SharedWithEmailAddress = "james.fulford.temporary@outlook.com",
                    SharedWithFirstName = "James",
                    SharedWithLastName = "Fulford",
                    SharedDate = DateTime.Now,
                },
                // Test requesting a movie (2)
                new Movie()
                {
                    Title = "Star Trek IV: The Voyage Home",
                    Category = "Comedy",
                    Shareable = true,
                    BeingReturned = false,
                    Owner = "8e6d95903278-58d3-4b7f-8202-8e6d95903278",
                    OwnerDisplay = "NuclearWessels92",
                },
            };

            foreach (Movie movie in movies)
            {
                context.Movie.Add(movie);
            }

            context.SaveChanges();

            List<MovieRequest> requests = new List<MovieRequest>()
            {
                new MovieRequest()
                {
                    MovieID = movies[0].ID,
                    RequesterEmailAddress = "email@asdfasd.com",
                    RequesterFirstName = "RequesterFirstName",
                    RequesterLastName = "RequesterLastName",
                    RequesterID = "RequesterID",
                    TimeRequested = DateTime.Now,
                },
                new MovieRequest()
                {
                    MovieID = movies[0].ID,
                    RequesterEmailAddress = "2email@asdfasd.com",
                    RequesterFirstName = "RequesterFirstName2",
                    RequesterLastName = "RequesterLastName",
                    RequesterID = "RequesterID2",
                    TimeRequested = DateTime.Now,
                },
                new MovieRequest()
                {
                    MovieID = movies[0].ID,
                    RequesterEmailAddress = "3email@asdfasd.com",
                    RequesterFirstName = "RequesterFirstName",
                    RequesterLastName = "RequesterLastName3",
                    RequesterID = "RequesterID3",
                    TimeRequested = DateTime.Now,
                },
                new MovieRequest()
                {
                    MovieID = movies[0].ID,
                    RequesterEmailAddress = "Req1@asdf.com",
                    RequesterFirstName = "First",
                    RequesterLastName = "Lastly",
                    RequesterID = "Req1",
                    TimeRequested = DateTime.Now,
                },
                new MovieRequest()
                {
                    MovieID = movies[1].ID,
                    RequesterEmailAddress = "Req1@asdf.com",
                    RequesterFirstName = "First",
                    RequesterLastName = "Lastly",
                    RequesterID = "Req1",
                    TimeRequested = DateTime.Now,
                },
                new MovieRequest()
                {
                    MovieID = movies[2].ID,
                    RequesterEmailAddress = "Req1@asdf.com",
                    RequesterFirstName = "First",
                    RequesterLastName = "Lastly",
                    RequesterID = "Req1",
                    TimeRequested = DateTime.Now,
                },
            };

            foreach (MovieRequest req in requests)
            {
                context.Request.Add(req);
            }

            context.SaveChanges();
        }
    }
}
