using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW6MovieSharing.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HW6MovieSharing
{
    public class Startup
    {
        /// <summary>
        /// DI for Startup sequence
        /// </summary>
        /// <param name="configuration">Configuration of host</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration of host
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services
        /// </summary>
        /// <param name="services">Services to be configured</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MovieContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MovieContext"))
            );
        }

        /// <summary>
        /// Configure HTTP pipeline for ASP.NET
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        /// <param name="env">HostingEnvironment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
