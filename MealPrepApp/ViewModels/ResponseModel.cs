using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MealPrepApp.ViewModels
{
    public class ResponseModel
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
    }
}
