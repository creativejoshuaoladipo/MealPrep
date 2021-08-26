using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Data.Models.Domain
{
    public class Meal
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        public string Ingredient { get; set; }
        public int CalorieId { get; set; }
        [ForeignKey("CalorieId")]
        public Calorie Calorie { get; set; }

    }
}
