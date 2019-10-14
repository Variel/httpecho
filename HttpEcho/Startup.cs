using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpEcho.Models;
using HttpEcho.RouteConstraints;
using HttpEcho.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllersWithViews(options => options.SuppressAsyncSuffixInActionNames = true);

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<HttpEchoUser, IdentityRole>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;

                options.Lockout.AllowedForNewUsers = false;
                options.Lockout.MaxFailedAccessAttempts = Int32.MaxValue;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789-";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/auth/accessDenied";
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
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

            app.UseAuthentication();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name: "intro", pattern: "{controller=Home}/{action=Index}", areaName: "Intro",
                    constraints: new {hostname = new IntroHostNameRouteConstraint(baseHost)});

                endpoints.MapAreaControllerRoute("user", pattern: "{controller=Home}/{action=Index}", areaName: "User",
                    constraints: new { hostname = new UserHostNameRouteConstraint(baseHost) });

                endpoints.MapControllerRoute("endpoint", pattern: "{*url}",
                    defaults: new {controller = "Request", action = "Incoming"},
                    constraints: new { hostname = new EndpointHostNameRouteConstraint(baseHost) });
            });
        }
    }
}
