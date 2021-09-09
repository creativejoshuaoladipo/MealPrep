using MealPrepUI.Models;
using MealPrepUI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MealPrepUI.Services
{
    public class ProductService : BaseService, IProductService
    {
        public IHttpClientFactory _httpClient { get; set; }
        public ProductService(IHttpClientFactory httpClient): base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequestModel
            {
                ApiType = SD.ApiTypes.GET,
                Url = SD.ProductAPIBaseURL + SD.ProductControllerRoute
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int productId)
        {
            return await this.SendAsync<T>(new ApiRequestModel
            {
                ApiType = SD.ApiTypes.GET,
                Url = SD.ProductAPIBaseURL + SD.ProductControllerRoute + productId
                
            });
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequestModel
            {
                ApiType = SD.ApiTypes.POST,
                Url = SD.ProductAPIBaseURL + SD.ProductControllerRoute,
                Data = productDto,


            });

        }


        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequestModel
            {
                ApiType = SD.ApiTypes.PUT,
                Url = SD.ProductAPIBaseURL + SD.ProductControllerRoute,
                Data = productDto,


            });

        }
        public async Task<T> DeleteProductAsync<T>(int productId)
        {
            return await this.SendAsync<T>(new ApiRequestModel
            {
                ApiType = SD.ApiTypes.DELETE,
                Url = SD.ProductAPIBaseURL + SD.ProductControllerRoute + productId,

            });
        }
    }
}
