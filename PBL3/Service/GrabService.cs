using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MySqlConnector;
using PBL3.Models;
using PBL3.DTO;
using System.Data;
namespace PBL3.Service
{public class GrabService
    {
        private readonly IConfiguration _iconfiguration;
        public GrabService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_iconfiguration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<OrderDetail>> GetGrabData(int IDGrab)
        {
            var orderDetails = new List<OrderDetail>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            var sql = new MySqlCommand("SELECT * FROM ORDERDETAIL WHERE IDGrab=@IDGrab", conn);
            sql.Parameters.AddWithValue("@IDGrab", IDGrab);
            using (var reader = await sql.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var orderDetail = new OrderDetail();
                    orderDetail.IDOrder = reader.GetInt32("IDOrder");
                    orderDetail.IDCustomer = reader.GetString("IDCustomer");
                    orderDetail.IDFood = reader.GetString("IDFood");
                    orderDetail.IDRes = reader.GetString("IDRes");
                    orderDetail.Quantity = reader.GetInt32("Quantity");
                    orderDetail.TotalPrice = reader.GetInt32("TotalPrice");
                    orderDetail.FoodName = reader.GetString("FoodName");
                    orderDetail.RestaurantName = reader.GetString("RestaurantName");
                    orderDetail.Url_image = reader.GetString("Url_image");
                    orderDetail.Status_Restaurant = reader.GetString("Status_Restaurant");
                    orderDetail.Status_User = reader.GetString("Status_User");
                    orderDetail.OrderTime = reader.GetDateTime("OrderTime");
                    orderDetail.OrderConfirmedTime = reader.GetDateTime("OrderConfirmedTime");
                    orderDetail.IDGrab = reader.GetInt32("IDGrab");
                    orderDetail.Phone = reader.GetString("Phone");
                    orderDetail.Address = reader.GetString("Address");
                    orderDetails.Add(orderDetail);
                }
            }
            return orderDetails;
        }
        public async Task<bool> PostTKGrab(ThongkeGrabDTO thongkeGrabDTO)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            string sql = "INSERT INTO ThongkeGrab (CusConfirmedTime, OrderTime, OrderConfirmedTime, Revenue, IDGrab) " +
                         "VALUES (@CusConfirmedTime, @OrderTime, @OrderConfirmedTime, @Revenue, @IDGrab)";

            var cmd = new MySqlCommand(sql, conn);
            DateTime datetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            cmd.Parameters.AddWithValue("@CusConfirmedTime", datetime);
            cmd.Parameters.AddWithValue("@OrderTime", thongkeGrabDTO.OrderTime);
            cmd.Parameters.AddWithValue("@OrderConfirmedTime", thongkeGrabDTO.OrderConfirmedTime);
            cmd.Parameters.AddWithValue("@Revenue", thongkeGrabDTO.Revenue);
            cmd.Parameters.AddWithValue("@IDGrab", thongkeGrabDTO.IDGrab);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
        public async Task<List<ThongkeGrabDTO>> getthongkegrab(int IDGrab)
        {
            var thongkeGrabList = new List<ThongkeGrabDTO>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            string sql = "SELECT* FROM ThongkeGrab WHERE IDGrab=@IDGrab";
            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDGrab", IDGrab);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var thongkeGrab = new ThongkeGrabDTO
                    {
                        CusConfirmedTime = reader.GetDateTime("CusConfirmedTime"),
                        OrderTime = reader.GetDateTime("OrderTime"),
                        OrderConfirmedTime = reader.GetDateTime("OrderConfirmedTime"),
                        Revenue = reader.GetInt32("Revenue"),
                        IDGrab = reader.GetInt32("IDGrab")
                    };
                    thongkeGrabList.Add(thongkeGrab);
                }
            }
            return thongkeGrabList;

        }
        public async Task<List<Grab>> GetAllGrab()
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            string sql = "SELECT* FROM GRAB";
            using var cmd = new MySqlCommand(sql, conn);
            var ListGrab = new List<Grab>();
            using var data = cmd.ExecuteReader();
            while (data.Read())
            {
                Grab x = new Grab();
                x.IDGrab = data.GetInt32("IDGrab");
                x.Address = data.GetString("Address");
                x.Name = data.GetString("Name");
                x.Phone = data.GetString("Phone");
                x.Pass = data.GetString("Pass");
                x.Role = data.GetString("Role");
                ListGrab.Add(x);
            }
            return ListGrab;
        }
      public async Task<bool> DeleteGrab(int IDGrab)
{
    using var conn = GetConnection();
    await conn.OpenAsync();
  string sql1 = "DELETE FROM Thongkegrab WHERE IDGrab = @IDGrab";
        using var cmd1 = new MySqlCommand(sql1, conn);
        cmd1.Parameters.AddWithValue("@IDGrab", IDGrab);
        await cmd1.ExecuteNonQueryAsync();
    string sql = "DELETE FROM Grab WHERE IDGrab = @IDGrab";
    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@IDGrab", IDGrab); 

    int rowsAffected = await cmd.ExecuteNonQueryAsync(); 

    return rowsAffected > 0; 
}



}
    
}
