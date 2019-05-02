using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HW6MovieSharing.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HW6MovieSharing
{
    /// <summary>
    /// Entrance to HW6MovieSharing program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start Movie host
        /// </summary>
        /// <param name="args">Args for starting movie host</param>
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHost(args);

            // Seed the database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    MovieContext context = services.GetRequiredService<MovieContext>();
                    Seeds.Sow(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "*** ERROR *** An error occurred while seeding the database. *** ERROR ***");
                }
            }
            host.Run();
        }

        /// <summary>
        /// Helper function to build a webhost with HW6MovieSharing Startup sequence
        /// </summary>
        /// <param name="args">Args passed on starting host</param>
        /// <returns>A built web host</returns>
        private static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
