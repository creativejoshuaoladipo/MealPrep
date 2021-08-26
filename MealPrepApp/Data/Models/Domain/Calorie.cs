using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Data.Models.Domain
{
    public class Calorie
    {
        public int Id { get; set; }
        public int Carbohydrate { get; set; }   
        public int Protein { get; set; }   
        public int Fats { get; set; }   
    }
}
