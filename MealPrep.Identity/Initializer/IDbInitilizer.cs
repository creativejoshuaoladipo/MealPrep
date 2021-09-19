using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrep.Identity.Initializer
{
    public interface IDbInitializer
    {
        public void Initialize();
    }
}
