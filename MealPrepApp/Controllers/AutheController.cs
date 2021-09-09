﻿using MealPrepApp.Data.Models.Identity;
using MealPrepApp.DTOs;
using MealPrepApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AutheController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("signup")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> SignUp([FromBody] SignUpVM model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var addedUser = await _userManager.FindByNameAsync(user.UserName);

                    //  var roleResult = await _userManager.AddToRoleAsync(addedUser, "Administrators");

                }

                UserDto userDto = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return Ok(new ResponseModel
                {
                    Data = userDto,
                    Message = "User was created sucessfully",
                    HttpStatus = HttpStatusCode.Created
                });

            }

            return BadRequest(new ResponseModel
            {
                Data = null,
                Message = "User was not created successfully",
                HttpStatus = HttpStatusCode.BadRequest
            });
        }



        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        [ProducesResponseType(typeof(ResponseModel), 400)]
        public async Task<IActionResult> Authenticate([FromBody] SignInVM model)
        {
            string token;
            string expiration;
            try
            {
                ApplicationUser user = null;
                user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return NotFound(new ResponseModel
                    {
                        HttpStatus = HttpStatusCode.NotFound,
                        Message = "User does not exist",
                        Data = null
                    });
                }
                else
                {
                    (token, expiration) = GenerateAccessToken(user);
                }
            }
            catch (Exception ex)
            {


                return BadRequest(new ResponseModel
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = $"Something went wrong {ex.Message} ",
                    Data = null
                });
            }

            var response = new ResponseModel
            {
                HttpStatus = HttpStatusCode.OK,
                Message = "user authenticated successfully",
                Data = new AuthenticationResultModel { AccessToken = token, ExpireInSeconds = expiration }
            };

            return Ok(response);
        }
        [NonAction]
        private (string, string) GenerateAccessToken(ApplicationUser user)
        {
            var claims = new List<Claim>();

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("email", user.Email),
                new Claim("phoneNumber", user.PhoneNumber),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("role", "Administrators"),

            });
            //  var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:JwtBearer:SecretKey").Value));

            var audience = _configuration.GetSection("Authentication:JwtBearer:Audience").Value;
            var issuer = _configuration.GetSection("Authentication:JwtBearer:Issuer").Value;
            var expiration = TimeSpan.FromMinutes(Convert.ToInt32(_configuration.GetSection("Authentication:JwtBearer:AccessExpiration").Value));
            var signInCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expiration),
                signingCredentials: signInCredentials);


            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return (token, expiration.TotalSeconds.ToString());
        }


        [HttpGet("get-users")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userManager.Users.ToList();

                var userList = users.Select(u =>
                {
                    var usr = new UserDto
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        PhoneNumber = u.PhoneNumber
                    };
                    return usr;
                });
                var response = new ResponseModel
                {
                    HttpStatus = HttpStatusCode.OK,
                    Message = "List of Users",
                    Data = userList
                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel
                {
                    HttpStatus = HttpStatusCode.BadRequest,
                    Message = $"Something went wrong {ex.Message}",
                    Data = null
                });
            }
        }

    }
}
