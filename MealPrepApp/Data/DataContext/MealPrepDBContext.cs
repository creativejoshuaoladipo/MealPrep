using MealPrepApp.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Data.DataContext
{
    public class MealPrepDBContext : DbContext
    {
        public MealPrepDBContext(DbContextOptions<MealPrepDBContext> options): base(options)
        {

          
        }

        public DbSet<Calorie> Calories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Product> Products { get; set; }

        

    }
}
