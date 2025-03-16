using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PBL3.Service;
namespace PBL3.Controllers
{
    [Route("api/auth")]
        [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly AuthService _authservice;
        public AuthController(AuthService authservice)
        {
            _authservice = authservice;
        }
        [HttpPost("customer/login")]
        public IActionResult Login([FromBody] JsonElement jsonElement)
        {
            string name = jsonElement.GetProperty("Name").GetString();
            string pass = jsonElement.GetProperty("Pass").GetString();
            var Customerid = _authservice.Login(name, pass);
            if (Customerid != null)
            {
                return Ok(new { Message = "LoginSuccessful", IDCustomer = Customerid });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid Message or Password" });
            }
        }
    }
}
