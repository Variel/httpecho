using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpEcho
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            var baseHost = config["BaseHost"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name: "base", pattern: "{controller=Home}/{action=Index}", areaName: "Intro",
                    constraints: new {hostname = new HostNameRouteConstraint(new HostNameRouteConstraintOptions
                    {
                        AllowPrimaryDomain = true,
                        AllowSubdomain = false,
                        PrimaryDomain = baseHost,
                        RouteName = "base"
                    })});

                endpoints.MapControllerRoute("reserved", pattern: "$/{controller}/{action}",
                    defaults: new {action = "Index"},
                    constraints: new
                    {
                        hostname = new HostNameRouteConstraint(new HostNameRouteConstraintOptions
                        {
                            AllowPrimaryDomain = false,
                            AllowSubdomain = true,
                            PrimaryDomain = baseHost,
                            RouteName = "reserved"
                        })
                    });

                endpoints.MapControllerRoute("echo", pattern: "{*url}",
                    defaults: new {controller = "Request", action = "Incoming"},
                    constraints: new
                    {
                        url = new HostNameRouteConstraint(new HostNameRouteConstraintOptions
                        {
                            AllowPrimaryDomain = false,
                            AllowSubdomain = true,
                            PrimaryDomain = baseHost,
                            RouteName = "echo"
                        })
                    });
            });
        }
    }
}
