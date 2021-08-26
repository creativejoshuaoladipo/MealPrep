using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Data.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
    }
}
