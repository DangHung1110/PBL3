
using Microsoft.AspNetCore.Mvc;
using PBL3.Service;
using PBL3.Models;
namespace PBL3.Controllers
{
    [Route("Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {


        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("restaurantwait")]
        public async Task<IActionResult> GetRestaurantWait()
        {
            try
            {
                var restaurantList = await _adminService.GetRestaurantWait();
                return Ok(restaurantList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpDelete("deletewaitresdata/{id}")]
        public async Task<IActionResult> Deletewaitresdata(int id)
        {
            try
            {
                var result = await _adminService.Deletewaitresdata(id);
                if (result)
                {
                    return Ok(new { Message = "Delete Successful" });
                }
                else
                {
                    return NotFound(new { Message = "Restaurant not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpGet("grabwait")]
        public async Task<IActionResult> GetGrabData()
        {
            try
            {
                var grablist = await _adminService.GetGrabData();
                return Ok(grablist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
        [HttpPost("grab/signupingrabafterwait")]
        public async Task<IActionResult> signupingrabafterwait([FromBody] Grab grab)

        {
            bool Check = await _adminService.signupingrabafterwait(grab);
            if (Check)
            {
                return Ok(new { Message = "SignUpSuccessful" });
            }
            else
            {
                return BadRequest(new { Message = "SignUpFail", ErrorCode = Check });
            }
        }
        [HttpDelete("deletewaitgrabdata/{id}")]
        public async Task<IActionResult> DeleteGrabData(int id)
        {
            try
            {
                var result = await _adminService.DeleteGrabData(id);
                if (result)
                {
                    return Ok(new { Message = "Delete Successful" });
                }
                else
                {
                    return NotFound(new { Message = "Grab data not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }
}