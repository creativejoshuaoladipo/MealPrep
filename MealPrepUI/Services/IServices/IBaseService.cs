using MealPrepApp.Models;
using MealPrepUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepUI.Services.IServices
{
    public interface IBaseService
    {

        ResponseModel responseModel { get; set; }

        Task<T> SendAsync<T>(ApiRequestModel request);

    }
}
