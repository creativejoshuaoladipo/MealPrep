using MealPrepApp.Models;
using MealPrepUI.Models;
using MealPrepUI.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepUI.Services
{
    public class BaseService : IBaseService
    {
        public ResponseModel responseModel { get; set; }
        protected IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseModel();
            _httpClient = httpClient;
        }
       

        public async Task<T> SendAsync<T>(ApiRequestModel apiRequest)
        {
            var client = _httpClient.CreateClient("MealPrepAPI");

            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept","application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();

            if(apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), 
                                                     Encoding.UTF8, "application/json");
            }


            if(apiRequest.ApiType == SD.ApiTypes.GET)
            {
                message.Method = HttpMethod.Get;
            }
            else if (apiRequest.ApiType == SD.ApiTypes.POST)
            {
                message.Method = HttpMethod.Post;
            }
            else if (apiRequest.ApiType == SD.ApiTypes.PUT)
            {
                message.Method = HttpMethod.Put;
            }
            else
            {
                message.Method = HttpMethod.Delete;

            }

            HttpResponseMessage apiResponseMessage = null;

            apiResponseMessage = await client.SendAsync(message);

            var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
            return apiResponseDto;

        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
