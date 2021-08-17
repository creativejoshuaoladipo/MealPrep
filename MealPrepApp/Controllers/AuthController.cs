using Microsoft.AspNetCore.Http;
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

        [HttpGet("sign-up")]
        public IActionResult SignUp()
        {
            return Ok();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
