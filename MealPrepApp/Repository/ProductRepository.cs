using AutoMapper;
using MealPrepApp.Data.DataContext;
using MealPrepApp.Data.Models.Domain;
using MealPrepApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MealPrepDBContext _dbContext;
        private IMapper _map;

        public ProductRepository(MealPrepDBContext dbContext, IMapper map)
        {
            _dbContext = dbContext;
            _map = map;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var productFromDB = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);

            Product createdProduct = _map.Map<ProductDto, Product>(productDto);

            if (productFromDB == null)
            {

                _dbContext.Products.Add(createdProduct);
            }
            else
            {
                _dbContext.Products.Update(createdProduct);

            }

           await _dbContext.SaveChangesAsync();
            return  _map.Map<Product, ProductDto>(createdProduct);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if(product == null)
            {
                return false;
            } 
            
            _dbContext.Products.Remove(product);
           await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var products = await _dbContext.Products.FirstOrDefaultAsync(p=> p.Id == productId );
            var productDto = _map.Map<ProductDto>(products);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            var productListDto = _map.Map<IEnumerable<ProductDto>> (products);
            return productListDto;
        }
    }
}
