using MealPrepApp.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.DTOs
{
    public class ProductDto
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string Description { get; set; }
            public string CategoryName { get; set; }
            public string ImageUrl { get; set; }
        
    }
}
