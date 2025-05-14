using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Data.SqlClient;
using MySqlConnector;
using PBL3.Service;
using PBL3.Models;
using PBL3.DTO;
using System.Collections.Generic;

namespace PBL3.Controllers
{
    [Route("api/Grab")]
    [ApiController]
    public class GrabController : ControllerBase
    {
        private readonly GrabService _grabservice;

        public GrabController(GrabService grabservice)
        {
            _grabservice = grabservice;
        }
        [HttpGet("ThongkeGrab/{IDGrab}")]
        public async Task<IActionResult> GetOrderDetails(int IDGrab)
        {
            try
            {
                var orderDetails = await _grabservice.GetGrabData(IDGrab);
                if (orderDetails != null && orderDetails.Count > 0)
                {
                    return Ok(orderDetails);
                }
                else
                {
                    return NotFound(new { Message = "No order details found for this restaurant" });
                }
            }
            catch (Exception ex)
            { Console.WriteLine("Lỗi server: " + ex.Message);
    return StatusCode(500, new { Message = "Server Error", Error = ex.Message }); }}

    }
}
