using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PBL3.DTO;
using PBL3.Models;
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
            grab.IDGrabWait = data.GetInt32("IDGrabWait");
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
    public async Task<bool> signupingrabafterwait(Grab grab)
    {    Console.WriteLine("IDGrab " + grab.IDGrab);
        using var conn = GetConnection();
        await conn.OpenAsync();
        string sql = "INSERT INTO GRAB (Name, Phone, Pass,Role) VALUES ( @Name, @Phone, @Pass,@Role)";
        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Name", grab.Name);

        cmd.Parameters.AddWithValue("@Phone", grab.Phone);
        cmd.Parameters.AddWithValue("@Pass", grab.Pass);
        cmd.Parameters.AddWithValue("@Role", grab.Role);
        int rowsAffected = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine("Rows affected: " + rowsAffected);
            Console.WriteLine("Rows affected: " + grab.IDGrab);
        if (rowsAffected > 0)
        {
            string deleteSql = "DELETE FROM GRABWAIT WHERE IDGrabWait = @IDGrabWait";
            using var deleteCmd = new MySqlCommand(deleteSql, conn);
            deleteCmd.Parameters.AddWithValue("@IDGrabWait", grab.IDGrab);
            await deleteCmd.ExecuteNonQueryAsync();
            return true;
        }
        return false;
    }

}