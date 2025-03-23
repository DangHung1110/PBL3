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
    private readonly string baseurl = "http://localhost:5000/images/";
    
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
    public void AddFood(FoodUpLoadDTO foodUpLoadDTO)
    {
        using var conn = GetConnection();
        conn.Open();
        var cmd = new MySqlCommand("INSERT INTO FOOD(IDFood,IDRes,Name,Price,Url_Image) VALUES(@IDFood,@IDR,@Name,@Price,@Url_Image)", conn);
        cmd.Parameters.AddWithValue("@IDFood", Food.getID());
        cmd.Parameters.AddWithValue("@IDR", foodUpLoadDTO.IDRes);
        cmd.Parameters.AddWithValue("@Name", foodUpLoadDTO.Name);
        cmd.Parameters.AddWithValue("@Price", foodUpLoadDTO.Price);
        cmd.Parameters.AddWithValue("@Url_Image", ImageDeal(foodUpLoadDTO));
        cmd.ExecuteNonQuery();
    }



}
