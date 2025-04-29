using MySqlConnector;
using PBL3.DTO;
using PBL3.Models;
namespace PBL3.Service;

using System;
using System.IO;
using System.Data.SqlClient;

public class RestaurantService
{
    private readonly string _uploadFolder = "wwwroot/Uploads";
    private readonly string baseurl = "http://localhost:5299/Uploads/";


    private readonly IConfiguration _iconfiguration;
    public RestaurantService(IConfiguration iconfiguration)
    {
        _iconfiguration = iconfiguration;
    }
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_iconfiguration.GetConnectionString("DefaultConnection"));
    }
    public string ImageDeal(FoodUpLoadDTO fooduploadDTO)
    {

        if (!Directory.Exists(_uploadFolder))
        {
            Directory.CreateDirectory(_uploadFolder);
        }
        string filename = Guid.NewGuid().ToString() + Path.GetExtension(fooduploadDTO.Url_Image.FileName);
        string path = Path.Combine(_uploadFolder, filename);
        using (var stream = System.IO.File.Create(path))
        {
            fooduploadDTO.Url_Image.CopyTo(stream);
        }

        string urldb = baseurl + filename;
        return urldb;

    }

    public string GenerateNextFoodID()
    {
        using var conn = GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("SELECT IDFood FROM FOOD ORDER BY IDFood DESC LIMIT 1", conn);
        var lastId = cmd.ExecuteScalar()?.ToString();

        if (string.IsNullOrEmpty(lastId))
            return "M001";

        int nextNumber = int.Parse(lastId.Substring(1)) + 1;
        return $"M{nextNumber:D3}";
    }


    public void AddFood(FoodUpLoadDTO foodUpLoadDTO)
    {
        using var conn = GetConnection();
        conn.Open();

        string newID = GenerateNextFoodID();
        string imgUrl = ImageDeal(foodUpLoadDTO);

        var cmd = new MySqlCommand(@"
    INSERT INTO FOOD(IDFood, Name, PRICE, IDRes, Discount, Category, Url_Image, Quantity)
    VALUES(@IDFood, @Name, @PRICE, @IDRes, @Discount, @Category, @Url_Image, @Quantity)", conn);

        cmd.Parameters.AddWithValue("@IDFood", newID);
        cmd.Parameters.AddWithValue("@Name", foodUpLoadDTO.Name);
        cmd.Parameters.AddWithValue("@PRICE", foodUpLoadDTO.Price);
        cmd.Parameters.AddWithValue("@IDRes", foodUpLoadDTO.IDRes);
        cmd.Parameters.AddWithValue("@Discount", foodUpLoadDTO.Discount);
        cmd.Parameters.AddWithValue("@Category", foodUpLoadDTO.Category);
        cmd.Parameters.AddWithValue("@Url_Image", imgUrl);
        cmd.Parameters.AddWithValue("@Quantity", foodUpLoadDTO.Quantity);

        cmd.ExecuteNonQuery();
    }

    public List<Food> GetFoods_DoAn()
    {
        List<Food> foods = new List<Food>();
        using var cnn = GetConnection();
        cnn.Open();
        var cmd = new MySqlCommand("SELECT * FROM FOOD WHERE Category = 'Đồ ăn'", cnn);
        using var rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            foods.Add(new Food
            {
                IDFood = rd.GetString("IDFood"),
                IDRes = rd.GetString("IDRes"),
                Name = rd.GetString("Name"),
                Price = rd.GetInt32("Price"),
                Discount = rd.GetInt32("Discount"),
                Category = rd.GetString("Category"),
                Url_Image = rd.GetString("Url_Image"),
                Quantity = rd.GetInt32("Quantity")
            });
        }

        return foods;
    }

    public List<Food> GetFoods_DoUong()
    {
        List<Food> foods = new List<Food>();
        using var cnn = GetConnection();
        cnn.Open();
        var cmd = new MySqlCommand("SELECT * FROM FOOD WHERE Category = 'Đồ uống'", cnn);
        using var rd = cmd.ExecuteReader();

        while (rd.Read())
        {
            foods.Add(new Food
            {
                IDFood = rd.GetString("IDFood"),
                IDRes = rd.GetString("IDRes"),
                Name = rd.GetString("Name"),
                Price = rd.GetInt32("Price"),
                Discount = rd.GetInt32("Discount"),
                Category = rd.GetString("Category"),
                Url_Image = rd.GetString("Url_Image"),
                Quantity = rd.GetInt32("Quantity")
            });
        }

        return foods;
    }


    public void DeleteFood(string idFood)
    {
        using var conn = GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("DELETE FROM FOOD WHERE IDFood = @IDFood", conn);
        cmd.Parameters.AddWithValue("@IDFood", idFood);

        cmd.ExecuteNonQuery();
    }

    public void UpdateFood(Food food)
    {
        using var conn = GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("UPDATE FOOD SET Name = @Name, Price = @Price, Discount = @Discount, Category = @Category, Quantity = @Quantity WHERE IDFood = @IDFood", conn);
        cmd.Parameters.AddWithValue("@IDFood", food.IDFood);
        cmd.Parameters.AddWithValue("@Name", food.Name);
        cmd.Parameters.AddWithValue("@Price", food.Price);
        cmd.Parameters.AddWithValue("@Discount", food.Discount);
        cmd.Parameters.AddWithValue("@Category", food.Category);
        cmd.Parameters.AddWithValue("@Quantity", food.Quantity);

        cmd.ExecuteNonQuery();
    }

    public List<Food> GetFoodsByRestaurant(string IDRes)
    {
        var foods = new List<Food>();
        using var conn = GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM FOOD WHERE IDRes = @IDRes", conn);
        cmd.Parameters.AddWithValue("@IDRes", IDRes);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            foods.Add(new Food
            {
                IDFood = rd.GetString("IDFood"),
                IDRes = rd.GetString("IDRes"),
                Name = rd.GetString("Name"),
                Price = rd.GetInt32("Price"),
                Discount = rd.GetInt32("Discount"),
                Category = rd.GetString("Category"),
                Url_Image = rd.GetString("Url_Image"),
                Quantity = rd.GetInt32("Quantity")
            });
        }
        return foods;
    }

    public List<Restaurant> GetAllRestaurants()
    {
        var restaurants = new List<Restaurant>();
        using var conn = GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM RESTAURANT", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            restaurants.Add(new Restaurant
            {
                IDRes = reader.GetString("IDRes"),
                Name = reader.GetString("Name"),
                Address = reader.GetString("Address"),
                Phone = reader.GetString("Phone"),
                Pass = reader.GetString("Pass"),
                Url_Image = reader.GetString("Url_Image")
            });
        }
        return restaurants;
    }

    public Food GetFoodById(string idFood)
    {
        using var conn = GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM FOOD WHERE IDFood = @IDFood", conn);
        cmd.Parameters.AddWithValue("@IDFood", idFood);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Food
            {
                IDFood = reader.GetString("IDFood"),
                IDRes = reader.GetString("IDRes"),
                Name = reader.GetString("Name"),
                Price = reader.GetInt32("Price"),
                Discount = reader.GetInt32("Discount"),
                Category = reader.GetString("Category"),
                Url_Image = reader.GetString("Url_Image"),
                Quantity = reader.GetInt32("Quantity")
            };
        }
        return null;
    }

    public Restaurant GetRestaurantById(string idRes)
    {
        using var conn = GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM RESTAURANT WHERE IDRes = @IDRes", conn);
        cmd.Parameters.AddWithValue("@IDRes", idRes);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Restaurant
            {
                IDRes = reader.GetString("IDRes"),
                Name = reader.GetString("Name"),
                Address = reader.GetString("Address"),
                Phone = reader.GetString("Phone"),
                Pass = reader.GetString("Pass"),
                Url_Image = reader.GetString("Url_Image")
            };
        }
        return null;
    }
    
    public async Task<List<OrderDetailDTO>> GetOrderDetailsByRestaurant(string idRes)
    {
        var orders = new List<OrderDetailDTO>();

        using (var conn = GetConnection())
        {
            await conn.OpenAsync();

            string query = @"
                SELECT 
                    IDOrder,
                    IDCustomer,
                    IDFood,
                    IDRes,
                    Url_Image,
                    FoodName,
                    RestaurantName,
                    Quantity,
                    TotalPrice
                FROM ORDERDETAIL
                WHERE IDRes = @idRes
            ";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idRes", idRes);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new OrderDetailDTO
                        {    IDOrder= reader.GetInt32("IDOrder"),
                            IDCustomer = reader.GetString("IDCustomer"),
                            IDFood = reader.GetString("IDFood"),
                            IDRes = reader.GetString("IDRes"),
                            Url_Image = reader.GetString("Url_Image"),
                            FoodName = reader.GetString("FoodName"),
                            RestaurantName = reader.GetString("RestaurantName"),
                            Quantity = reader.GetInt32("Quantity"),
                            TotalPrice = reader.GetInt32("TotalPrice")
                        });
                    }
                }
            }
        }

        return orders;
    }
}
