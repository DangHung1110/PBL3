using Microsoft.AspNetCore.Mvc;
using PBL3.Service;
using System.Text.Json;
using PBL3.Models;

namespace PBL3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // API đăng nhập
        /*HttpPost("login")]
        public IActionResult Login([FromBody] JsonElement jsonElement)
        {
            try
            {
                // Kiểm tra xem JSON có chứa "Name" và "Pass" không
                if (!jsonElement.TryGetProperty("Name", out JsonElement nameElement) ||
                    !jsonElement.TryGetProperty("Pass", out JsonElement passElement))
                {
                    return BadRequest(new { Message = "Missing Name or Pass" });
                }

                string name = nameElement.GetString();
                string pass = passElement.GetString();

                // Kiểm tra nếu name hoặc pass bị null hoặc rỗng
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pass))
                {
                    return BadRequest(new { Message = "Name or Pass cannot be empty" });
                }

                var customerId = _customerService.Login(name, pass);

                if (customerId != null)
                {
                    return Ok(new { Message = "Login Successful", IDCustomer = customerId });
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid Name or Password" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }   
    }*/
    [HttpPost("order")]
        public async Task<IActionResult> Order([FromBody] OrderDetail orderDetail)
        {
            try
            {
                bool result = await _customerService.Order(orderDetail); //  await
                if (result)
                {
                    return Ok(new { Message = "Order Successful" });
                }
                else
                {
                    return BadRequest(new { Message = "Order Failed - Validation failed or foreign key mismatch" });
                }
            }
            catch (Exception ex)
            {return StatusCode(500, new { Message = "Server Error", Error = ex.Message });
            }
        }

        [HttpDelete("order/{idOrder}")]
        public async Task<IActionResult> DeleteOrderDetail(int idOrder)
        {
            bool success = await _customerService.DeleteOrderDetail(idOrder);
            if (success)
            {
                return Ok(new { Message = "Delete successful" });
            }
            else
            {
                return NotFound(new { Message = "OrderDetail not found or already deleted" });
            }
        }
    }
}
