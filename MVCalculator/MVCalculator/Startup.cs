using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MVCalculator.Models;

namespace MVCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<MVCalculatorContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MVCalculatorContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                /*routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");*/

                routes.MapRoute(
                  "Add",
                  "Calculator/Add", // URL pattern
                   new { controller = "Calculator", action = "Add" }
                );

                routes.MapRoute(
                  "Sub",
                  "Calculator/Sub", // URL pattern
                   new { controller = "Calculator", action = "Sub" }
                );

                routes.MapRoute(
                  "Mult",
                  "Calculator/Mult", // URL pattern
                   new { controller = "Calculator", action = "Mult" }
                );

                routes.MapRoute(
                  "Add",
                  "Calculator/Div", // URL pattern
                   new { controller = "Calculator", action = "Div" }
                );

                routes.MapRoute(
                  "Sqrt",
                  "Calculator/Sqrt", // URL pattern
                   new { controller = "Calculator", action = "Sqrt" }
                );

                routes.MapRoute(
                  "Query",
                  "Journal/Query", // URL pattern
                   new { controller = "Journal", action = "Query" }
                );

                routes.MapRoute(
                  "Store",
                  "Journal/Store", // URL pattern
                   new { controller = "Journal", action = "Store" }
                );

                routes.MapRoute(
                "PageNotFound",
                "{*catchall}",
                new { controller = "Error", action = "PageNotFound" }
                );
            });
 
        }
    }
}
