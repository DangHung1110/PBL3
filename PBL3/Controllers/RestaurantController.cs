﻿using Microsoft.AspNetCore.Http;
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
    [Route("api/Restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantservice;

        public RestaurantController(RestaurantService restaurantservice)
        {
            _restaurantservice = restaurantservice;
        }

        // CREATE
        [HttpPost("uploadfood")]
        public IActionResult AddfoodIn([FromForm] FoodUpLoadDTO fooduploadDTO)
        {
            _restaurantservice.ImageDeal(fooduploadDTO);
            if (string.IsNullOrEmpty(fooduploadDTO.Name) || fooduploadDTO.Url_Image == null || fooduploadDTO.Price < 0)
            {
                return BadRequest(new { Message = "Upload Fail" });
            }

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

        // READ
        [HttpGet("DoAn")]
        public IActionResult GetAllFoods()
        {
            try
            {
                var foods = _restaurantservice.GetFoods_DoAn();
                return Ok(foods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("DoUong")]
        public IActionResult GetAllDrinks()
        {
            try
            {
                var foods = _restaurantservice.GetFoods_DoUong();
                return Ok(foods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("deletefood/{id}")]
        public IActionResult DeleteFood(string id)
        {
            try
            {
                _restaurantservice.DeleteFood(id);
                return Ok(new { Message = "Food deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Delete failed", Error = ex.Message });
            }
        }

        // UPDATE
        [HttpPut("updatefood")]
        public IActionResult UpdateFood([FromBody] Food food)
        {
            if (string.IsNullOrEmpty(food.Name) || food.Price < 0)
            {
                return BadRequest(new { Message = "Invalid food data." });
            }

            try
            {
                _restaurantservice.UpdateFood(food);
                return Ok(new { Message = "Food updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Update failed", Error = ex.Message });
            }
        }

        [HttpGet("foods/{IDRes}")]
        public IActionResult GetFoodsByRestaurant(string IDRes)
        {
            var foods = _restaurantservice.GetFoodsByRestaurant(IDRes);
            return Ok(foods);
        }

        [HttpGet("GetAllRestaurants")]
        public IActionResult GetAllRestaurants()
        {
            var restaurants = _restaurantservice.GetAllRestaurants();
            return Ok(restaurants);
        }

    }
}
