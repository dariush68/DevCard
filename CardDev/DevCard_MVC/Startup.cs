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
            //-- custom middleware --//
            app.UseCustomLogger();


            //-- UseWhen --//
            app.UseWhen(context => context.Request.Query.ContainsKey("title"), builder =>
            {
                builder.Run(async context =>
                {
                    var title = context.Request.Query["title"];
                    await context.Response.WriteAsync($"title is: {title}");
                });
            });

            //-- MapWhen --//
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), builder =>
            {
                builder.Run(async context =>
                {
                    var branch = context.Request.Query["branch"];
                    await context.Response.WriteAsync($"branch is: {branch}");
                });
            });


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
            app.Map("/products", appBuilder =>
            {
                appBuilder.Map("/Details", builder =>
                {
                    builder.Run(async context =>
                    {
                        await context.Response.WriteAsync("this is product detail page");
                    });
                });

                appBuilder.Use(async (context, next) =>
                {
                    var name = context.Request.Query["name"];
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        context.Items.Add("name", name);
                    }
                    await next.Invoke();
                });

                appBuilder.Run(async context =>
                {
                    //context.Items.TryGetValue("name", out var name);
                    var name = context.Items["name"];
                    if (!string.IsNullOrWhiteSpace(name.ToString()))
                    {
                        name = "Dariush";
                    }
                    await context.Response.WriteAsync($"my name is: {name}");
                });
            });


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
