using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Data.SqlClient;
using MySqlConnector;
using PBL3.Service;
using PBL3.Models;
using Microsoft.EntityFrameworkCore.Storage;
using PBL3.DTO;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
         
        private readonly RestaurantService _restaurantservice;
        public RestaurantController(RestaurantService restaurantservice)
        {
            _restaurantservice = restaurantservice;
        }
        [HttpPost("uploadfood")]
        public IActionResult AddfoodIn([FromForm] FoodUpLoadDTO fooduploadDTO)
        {
            

            _restaurantservice.ImageDeal(fooduploadDTO);
            if (string.IsNullOrEmpty(fooduploadDTO.Name) || fooduploadDTO.Url_Image==null || fooduploadDTO.Price < 0)
            {

                return BadRequest(new { Message = "Upload Fail" });

            }
            else
            {
                try
                {
                    _restaurantservice.AddFood(fooduploadDTO);
                    return Ok(new { Message = "Upload Successful" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
                }
            }
        }
    }
}














