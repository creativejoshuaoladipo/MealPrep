using MealPrepApp.Data.Models.Identity;
using MealPrepApp.DTOs;
using MealPrepApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        public AuthController(Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            _userManager = userManager;         
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> SignUp([FromBody] SignUpVM signUpVM)
        {

            if (ModelState.IsValid)
            {

                var user = new User
                {
                    Email = signUpVM.Email,
                    EmailConfirmed = true,
                    FirstName = signUpVM.FirstName,
                    LastName = signUpVM.LastName,
                    PhoneNumber = signUpVM.PhoneNumber,
                    PhoneNumberConfirmed = true
                };

                var createdUser = await _userManager.CreateAsync(user, signUpVM.Password);

                if (createdUser.Succeeded)
                {

                    var addedUser = await _userManager.FindByNameAsync(user.UserName);
                    var userRole = await _userManager.AddToRoleAsync(addedUser, "Admin");

                }




                var userDto = new UserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                };


                return Ok(new ResponseModel
                {
                    Data = userDto,
                    Message = "User Created Successfully",
                    HttpStatus = System.Net.HttpStatusCode.OK

                }
                   );

            }

            return BadRequest(new ResponseModel
            {
                Data = null,
                Message = "User was not Created",
                HttpStatus = System.Net.HttpStatusCode.BadRequest
            });
           
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
