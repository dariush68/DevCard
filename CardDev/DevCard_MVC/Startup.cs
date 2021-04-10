using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevCard_MVC
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
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //-- Use --//
            app.Use(async (context, next) =>
            {
                //-- some logic --//

                //-- context is shared dictionary between all middleware --//
                context.Items.Add("name", "Dariush");

                //-- terminate --//
                // await context.Response.WriteAsync("this is a use middleware");

                await next.Invoke();

                //-- run code after al middleware --//
            });

            app.Use(async (context, next) =>
            {
                var id = context.Request.Query["id"];

                await next.Invoke();
            });
            
            //-- Map --//

            //-- Run (last middleware, terminator middleware, after that return response) --//
            app.Run(async context =>
            {
                var name = context.Items["name"];
                await context.Response.WriteAsync("Run Executed Successfully");
            });



            #region Defualt Middleware

            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });*/

            #endregion


        }
    }
}
