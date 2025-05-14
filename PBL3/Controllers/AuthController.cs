using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PBL3.Service;
using PBL3.Models;
using PBL3.DTO;
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
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            string name =loginDTO.Name;
            string pass =loginDTO.Password;
           Object result = _authservice.Login(name, pass);
            if (result != null && result.GetType().GetProperty("Role").GetValue(result).ToString()=="Customer")
            {
                return Ok(new { Message = "Login Successful", UserID=result.GetType().GetProperty("IDCustomer").GetValue(result),Role=result.GetType().GetProperty("Role").GetValue(result),UserName=result.GetType().GetProperty("Name").GetValue(result),Phone=result.GetType().GetProperty("Phone").GetValue(result),Address=result.GetType().GetProperty("Address").GetValue(result) });
            }
            else
            if(result != null && result.GetType().GetProperty("Role").GetValue(result).ToString()=="Restaurant")
            {
                return Ok(new { Message = "Login Successful", UserID=result.GetType().GetProperty("IDRes").GetValue(result),Role=result.GetType().GetProperty("Role").GetValue(result),UserName=result.GetType().GetProperty("Name").GetValue(result) });
            }
              else
            if(result != null && result.GetType().GetProperty("Role").GetValue(result).ToString()=="Grab")
            {
                return Ok(new { Message = "Login Successful", UserID=result.GetType().GetProperty("IDGrab").GetValue(result),Role=result.GetType().GetProperty("Role").GetValue(result),UserName=result.GetType().GetProperty("Name").GetValue(result) });
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
