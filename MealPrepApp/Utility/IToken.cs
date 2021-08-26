using MealPrepApp.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Utility
{
    public interface IToken
    {
        (string, string) GenerateAccessToken(User user);


    }
}
