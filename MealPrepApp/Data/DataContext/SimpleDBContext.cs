using MealPrepApp.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Data.DataContext
{
    public class SimpleDBContext : IdentityDbContext<ApplicationUser, Role, int>
    {


        public SimpleDBContext(DbContextOptions<SimpleDBContext> dbContextOptions) : base(dbContextOptions)
        {



        }

        //  public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(u =>

            {
                u.ToTable("ApplicationUser");
                u.HasKey(p => p.Id);
                u.Property(p => p.PasswordHash).HasColumnName("Password").HasColumnType("varchar(250)");
                u.Property(p => p.PhoneNumber).HasColumnType("varchar(15)");
                u.Property(p => p.Email).HasColumnType("varchar(100)");
                u.Property(p => p.NormalizedEmail).HasColumnType("varchar(100)");
                u.Property(p => p.UserName).HasColumnType("varchar(100)");
                u.Property(p => p.NormalizedUserName).HasColumnType("varchar(100)");
                u.Property(p => p.ConcurrencyStamp).HasColumnType("varchar(100)");
                u.Property(p => p.FirstName).HasColumnType("varchar(100)");
                u.Property(p => p.LastName).HasColumnType("varchar(100)");

            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 1,
                FirstName = "Joshua",
                LastName = "Oladipo",
                PhoneNumber = "07060412288",
                Email = "joshuaoladipo29@gmail.com",
                UserName = "joshuaoladipo29@gmail.com"
            });


            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 2,
                FirstName = "Femi",
                LastName = "Adewale",
                PhoneNumber = "0807878777",
                Email = "joshuaoladipo29@gmail.com",
                UserName = "joshuaoladipo29@gmail.com"

            });

            builder.Entity<Role>().HasData(new Role
            {

                Id = 1,
                Name = "SuperAdmin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "SUPERADMIN"

            });


            builder.Entity<Role>().HasData(new Role
            {

                Id = 2,
                Name = "Admin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "ADMIN"

            });



        }





    }
}
