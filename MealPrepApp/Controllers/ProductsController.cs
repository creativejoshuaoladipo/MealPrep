using MealPrepApp.DTOs;
using MealPrepApp.Repository;
using MealPrepApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MealPrepApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _repository;
        protected ResponseModel _responseModel;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
            _responseModel = new ResponseModel();
        }


        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductDto productDto)
        {
            if (ModelState.IsValid)
            {

                var newProduct = await _repository.CreateUpdateProduct(productDto);

                var result = new ResponseModel
                {

                    HttpStatus = HttpStatusCode.OK,
                    Data = newProduct,
                    Message = "The product was created successfully"
                };

                return Ok(result);

            }

            return BadRequest(
                new ResponseModel
                {

                    HttpStatus = HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "There is an error something"
                }
                );
            
            
        }


        [HttpGet("get-product")]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                var product = await _repository.GetProducts();

                var result = new ResponseModel
                {
                    HttpStatus = HttpStatusCode.OK,
                    Message = "Product gotten successfully",
                    Data = product
                };
                return Ok(result);


            }
            catch (Exception ex)
            {
                var result = new ResponseModel
                {
                    HttpStatus = HttpStatusCode.InternalServerError,
                    Message = ex.ToString(),
                    Data = null
                };

                return BadRequest(result);
            }
        }


        [HttpGet("get-product-id/ {id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _repository.GetProductById(id);

                var result = new ResponseModel
                {
                    HttpStatus = HttpStatusCode.OK,
                    Data = product,
                    Message = $"The Product with id {id} was successfully found"
                };
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(
                   new ResponseModel
                    {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Data = null,
                    Message = $"There is an error something: {ex.ToString()}"
                     }
                     );
            }

        }


        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                
                var newProduct = await _repository.CreateUpdateProduct(productDto);

                var result = new ResponseModel
                {

                    HttpStatus = HttpStatusCode.OK,
                    Data = newProduct,
                    Message = "The product was created successfully"
                };

                return Ok(result);

            }

            return BadRequest(
                new ResponseModel
                {

                    HttpStatus = HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "There is an error something"
                }
                );
        }


        [HttpDelete("delete-product/{id}")]
       // [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            try
            {

                var productToBeDeleted = await _repository.DeleteProduct(id);

                var result = new ResponseModel
                {

                    HttpStatus = HttpStatusCode.OK,
                    Data = null,
                    Message = $"The product with id {id} was deleted successfully"
                };

                return Ok(result);

            }
            catch (Exception)
            {

                return BadRequest(
                              new ResponseModel
                              {

                                  HttpStatus = HttpStatusCode.BadRequest,
                                  Data = null,
                                  Message = "There is an error something"
                              }
                              );
            }

               
          
        }


    }
}
