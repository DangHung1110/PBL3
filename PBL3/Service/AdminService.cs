using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PBL3.DTO;

namespace PBL3.Service;

public class AdminService
{
    private readonly IConfiguration _configuration;
    public AdminService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
    public async Task<List<RESTAURANTWAITDTO>> GetRestaurantWait()
    {
        using var conn = GetConnection();
        await conn.OpenAsync();
        string sql = "SELECT* FROM  RESTAURANTWAIT";
        using var cmd = new MySqlCommand(sql, conn);
        var data = await cmd.ExecuteReaderAsync();
        var restaurantlist = new List<RESTAURANTWAITDTO>();
        while (data.Read())
        {
            RESTAURANTWAITDTO restaurant = new RESTAURANTWAITDTO();
            restaurant.IDRes = data.GetInt32("IDRes");
            restaurant.Name = data.GetString("Name");
            restaurant.Address = data.GetString("Address");
            restaurant.Phone = data.GetString("Phone");
            restaurant.Pass = data.GetString("Pass");
            restaurant.Url_Image = data.GetString("Url_image");
            restaurant.Url_Image2 = data.GetString("Url_image2");
            restaurant.Url_Image3 = data.GetString("Url_image3");

            restaurantlist.Add(restaurant);
        }
        return restaurantlist;
    }
    public async Task<bool> Deletewaitresdata(int id)
    {
        using var conn = GetConnection();
        await conn.OpenAsync();
        string sql = "DELETE FROM RESTAURANTWAIT WHERE IDRes = @IDRes";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDRes", id);
        int rowsAffected = await cmd.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }
    public async Task<List<GRABWAIT>> GetGrabData()
    {
        using var conn = GetConnection();
        await conn.OpenAsync();
        string sql = "SELECT* FROM GRABWAIT";
        using var cmd = new MySqlCommand(sql, conn);
        var ListGrab = new List<GRABWAIT>();
        var data = await cmd.ExecuteReaderAsync();
        while (data.Read())
        {
            GRABWAIT grab = new GRABWAIT();
            grab.IDRes = data.GetInt32("IDGrabWait");
            grab.Name = data.GetString("Name");
            grab.Address = data.GetString("Address");
            grab.Phone = data.GetString("Phone");
            grab.Pass = data.GetString("Pass");
            grab.Url_Image = data.GetString("Url_image");
            grab.Url_Image2 = data.GetString("Url_image2");

            ListGrab.Add(grab);
        }
        return ListGrab;

    }

}