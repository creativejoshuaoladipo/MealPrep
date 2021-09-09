using MealPrepApp.Data.Models.Identity;
using MealPrepApp.Data.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MealPrepApp.Utility;
using MealPrepApp.Mapper;
using AutoMapper;
using MealPrepApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace MealPrepApp
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

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meal Prep App", Version = "v1" });
            });


            services.AddDbContext<SimpleDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(50));
            });

            services.AddDbContext<MealPrepDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            //services.AddDbContext<SimpleDBContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //        sqlOptions => sqlOptions.EnableRetryOnFailure(50));
            //});

            //services.AddDbContext<MealPrepDBContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //        sqlOptions => sqlOptions.EnableRetryOnFailure(50));
            //});

            services.AddIdentity<ApplicationUser, Role>(options =>
            {

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<SimpleDBContext>().AddDefaultTokenProviders();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(cfg => {
                   cfg.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = Configuration["Authentication:JwtBearer:Issuer"],
                       ValidateAudience = true,
                       ValidAudience = Configuration["Authentication:JwtBearer:Audience"],
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:JwtBearer:SecretKey"]))
                   };

               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrators", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireClaim("role", "Administrators")
                    .Build());
            });

            services.AddTransient<IToken, Token>();

            IMapper mapper = MapperConfig.RegristerMapper().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IProductRepository, ProductRepository>();

           



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MealPrepApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
