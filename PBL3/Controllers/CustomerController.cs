using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using PBL3.Service;
using System.Text.Json;


namespace PBL3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        private readonly CustomerService _customerservice;
        public CustomerController(CustomerService Customerservice)
        {
            _customerservice = Customerservice;
        }
        [HttpPost("login")]
       public IActionResult Login([FromBody] JsonElement jsonElement)
        {
            string name =jsonElement.GetProperty("Name").GetString();
            string pass =jsonElement.GetProperty("Pass").GetString();
            var Customerid = _customerservice.Login(name, pass);
            if(Customerid!=null)
            {
                return Ok(new {Message="LoginSuccessful",IDCustomer=Customerid});
            }
            else
            {
                return Unauthorized(new { Message = "Invalid Message or Password" });
            }
        }
    }



}
