using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepUI
{
    public static class SD
    {

        public static string ProductAPIBaseURL { get; set; }

        public static string ProductControllerRoute { get; set; } = "api/products/";
        public enum ApiTypes 
        { 
        GET,
        POST,
        PUT,
        DELETE
        }





    }
}
