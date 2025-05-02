using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PBL3.Service;
using PBL3.Models;
namespace PBL3.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authservice;
        public AuthController(AuthService authservice)
        {
            _authservice = authservice;
        }
      

        [HttpPost("login")]
        public IActionResult Login([FromBody] JsonElement jsonElement)
        {
            string name = jsonElement.GetProperty("Name").GetString();
            string pass = jsonElement.GetProperty("Pass").GetString();
            string role = jsonElement.GetProperty("Role").GetString(); // Xác định đăng nhập từ đâu

            var userId = _authservice.Login(name, pass, role);
            if (userId != null)
            {
                return Ok(new { Message = "Login Successful", UserID = userId, Role = role });
            }
            return Unauthorized(new { Message = "Invalid Credentials" });
        }

        [HttpPost("restaurant/signup")]
        public IActionResult SignUp2([FromBody] Restaurant restaurant)
        {
            int Check = _authservice.Signup2(restaurant);
            if (Check == 0)
            {
                return Ok(new { Message = "SignUpSuccessful" });
            }
            else
            {
                return BadRequest(new { Message = "SignUpFail", ErrorCode = Check });
            }
        }
        [HttpPost("customer/signup")]
        public IActionResult Signup([FromBody] Customer customer)
        {
            int Check = _authservice.SignUp(customer);
            if (Check == 0)
            {
                return Ok(new { Message = "SignUpSuccessful" });
            }
            else
            {
                return BadRequest(new { Message = "SignUpFail", ErrorCode = Check });
            }

        }
    }
}
