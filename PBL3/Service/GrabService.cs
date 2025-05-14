using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MySqlConnector;
using PBL3.Models;
using System.Data;
namespace PBL3.Service
{public class GrabService{
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
                    orderDetail.IDGrab=reader.GetInt32("IDGrab");
                    orderDetail.Phone=reader.GetString("Phone");
        orderDetail.Address=reader.GetString("Address");
                    orderDetails.Add(orderDetail);
                }
            }
            return orderDetails;
        }
}
    
}
