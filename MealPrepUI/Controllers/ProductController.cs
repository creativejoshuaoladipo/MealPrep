using MealPrepApp.Models;
using MealPrepUI.Models;
using MealPrepUI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productServive)
        {
            _productService = productServive;
        }

        // GET: ProductController
        public async Task<ActionResult> GetAllProducts()
        {
           var apiResponse = await _productService.GetAllProductsAsync<ResponseModel>();

            //Create a List to the result;

            List<ProductDto> productList = new List<ProductDto>();

            if(apiResponse != null )
            {
                //Anytime you are Deserializing an Object always remeber to add the Generic bracket<>
                //<> is to specify the type of object you want to convert your json object to
                productList = JsonConvert.DeserializeObject<List<ProductDto>>( Convert.ToString(apiResponse.Data));
            }

            return View(productList);
        }

      
        //CRUD
        // GET: ProductController/Create
        public async Task<ActionResult> CreateProduct()
        {

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct(ProductDto productDto)
        {

            try
            {
                var apiProductDto = await _productService.CreateProductAsync<ProductDto>(productDto);

                return RedirectToAction(nameof(GetAllProducts));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> EditProduct(int productId)
        {
            var apiResponse = await _productService.GetProductByIdAsync<ResponseModel>(productId);

            if(apiResponse != null)
            {
                ProductDto product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(apiResponse.Data));
                return View(product);
            }

            return NotFound();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(ProductDto productDto)
        {       
            var apiResponse = await _productService.UpdateProductAsync<ResponseModel>(productDto);
            if (apiResponse != null)
            {
                return RedirectToAction(nameof(GetAllProducts));
            }

                return View(productDto);
           
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var apiResponse = await _productService.GetProductByIdAsync<ResponseModel>(id);

            if(apiResponse != null)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(apiResponse.Data));
                return View(product);
            }

            return NotFound();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProduct(ProductDto productDto)
        {
            var apiResponse = await _productService.DeleteProductAsync<ResponseModel>(productDto.Id);

            if (apiResponse != null)
            {
                return RedirectToAction(nameof(GetAllProducts));
            }

            return NotFound();
        }
    }
}
