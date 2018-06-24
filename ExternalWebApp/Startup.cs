using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExternalWebApp.Data;
using ExternalWebApp.Models;
using ExternalWebApp.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.IdentityModel.Tokens;

namespace ExternalWebApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {

                    googleOptions.ClientId = "595631049296-nn2lkcccn7k3iev5k4q33tqjllr9omih.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "0NacKl2pkBqBX1GnMyQ88c0n";

                })
                .AddOpenIdConnect("Company Authorization Server", options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ResponseType = "code id_token";
                    options.ClientSecret = "secret";
                    options.Scope.Add("email");
                    options.Scope.Add("offline_access");
                    options.ClientId = "mvc_external_app";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                });

        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
