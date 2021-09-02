using MealPrepUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepUI.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<T>> GetProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int productId);
        Task<T> CreateUpdateProductAsync<T>(ProductDto productDto);
        Task<T> DeleteProductAsync<T>(int productId);

    }
}
