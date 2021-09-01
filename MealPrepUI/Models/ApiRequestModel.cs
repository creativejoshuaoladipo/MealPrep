using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MealPrepUI.SD;

namespace MealPrepUI.Models
{
    public class ApiRequestModel
    {
        public ApiTypes ApiType { get; set; } = ApiTypes.GET;

        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

    }
}
