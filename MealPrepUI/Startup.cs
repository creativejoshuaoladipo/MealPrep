using MealPrepUI.Services;
using MealPrepUI.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepUI
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
            SD.ProductAPIBaseURL = Configuration["ServiceURLs: ProductAPI"];
            services.AddHttpClient<IProductService, ProductService>();
            services.AddSingleton<IProductService, ProductService>();

            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
             .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
             .AddOpenIdConnect("oidc", options =>
             {
                 options.Authority = Configuration["ServiceUrls:IdentityAPI"];
                 options.GetClaimsFromUserInfoEndpoint = true;
                 options.ClientId = "mango";
                 options.ClientSecret = "secret";
                 options.ResponseType = "code";
                 options.ClaimActions.MapJsonKey("role", "role", "role");
                 options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                 options.TokenValidationParameters.NameClaimType = "name";
                 options.TokenValidationParameters.RoleClaimType = "role";
                 options.Scope.Add("mango");
                 options.SaveTokens = true;

             });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
