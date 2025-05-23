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

        [HttpGet("GetFoodById/{IDFood}")]
        public IActionResult GetFoodById(string IDFood)
        {
            var food = _restaurantservice.GetFoodById(IDFood);
            if (food == null)
            {
                return NotFound(new { Message = "Food not found." });
            }
            return Ok(food);
        }

        [HttpGet("GetRestaurantById/{IDRes}")]
        public IActionResult GetRestaurantById(string IDRes)
        {
            var restaurant = _restaurantservice.GetRestaurantById(IDRes);
            if (restaurant == null)
            {
                return NotFound(new { Message = "Restaurant not found." });
            }
            return Ok(restaurant);
        }
        [HttpGet("GetOrderDetailsByRestaurant/{IDRes}")]
        public async Task<IActionResult> GetOrderDetailsByRestaurant(string IDRes)
        {
            var result = await _restaurantservice.GetOrderDetailsByRestaurant(IDRes);
            return Ok(result);
        }
        [HttpPost("Thongke")]
        public void AddTK(Thongke thongke)
        {
            try
            {
                _restaurantservice.AddTKData(thongke);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        [HttpPut("UpdateConfirmedTime/{IDOrder}")]
        public IActionResult UpdateConfirmedTime(int IDOrder)
        {
            try
            {
                _restaurantservice.UpdateConfirmedTime(IDOrder);
                return Ok(new { Message = "Order confirmed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Update failed", Error = ex.Message });
            }
        }
        [HttpPut("ChangeFoodNum/{id}")]
        public IActionResult ChangeFoodNum(string id, [FromBody] QuantityUpdateDTO dto)
        {  try{
                _restaurantservice.ChangeFoodNum(id,dto.Quantity);
                return Ok(new { Message = "Food quantity updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Update failed", Error = ex.Message });
            }
        }
        [HttpGet("GetFoodByName/{Name}")]
        public IActionResult GetFoodByName(String Name)
        {
            var food=_restaurantservice.GetFoodByName(Name);
            if (food == null)
            {
                return NotFound(new { Message = "Food not found." });
            }
            return Ok(food);
        }
        [HttpGet("GetTKData/{IDCustomer}")]
        public IActionResult GetTKData(string IDCustomer)
        {
            var result = _restaurantservice.GetTKData(IDCustomer);
            if (result == null)
            {
                return NotFound(new { Message = "No data found." });
            }
            return Ok(result);
        }
          [HttpPatch("updateFood/{idFood}")]
        public IActionResult UpdateFood(string idFood, [FromBody] FoodUpdateDTO updateData)
        {
            bool success = _restaurantservice.UpdateFoodPartial(idFood, updateData.Price, updateData.Quantity, updateData.Discount);

            if (!success)
                return BadRequest("No fields to update or invalid ID.");

            return Ok("Food updated successfully.");
        }
        [HttpGet("GetRevenue/{IDCus}")]
        public IActionResult GetRevenue (string IDCus)
        {
            var result=_restaurantservice.GetRevenue(IDCus);
            if (result == null)
            {
                return NotFound(new { Message = "No data found." });
            }
            return Ok(result);
        }
    }
}
