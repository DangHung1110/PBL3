using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MySqlConnector;
using PBL3.Models;
using System.Data;
namespace PBL3.Service
{
    public class CustomerService
    {
        private readonly IConfiguration _iconfiguration;
        public CustomerService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_iconfiguration.GetConnectionString("DefaultConnection"));
        }
        public async Task<bool> Order(OrderDetail order)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            // Kiểm tra tính hợp lệ
            string checkQuery = @"
        SELECT f.IDFood, c.IDCustomer, r.IDRes 
        FROM FOOD f
        JOIN RESTAURANT r ON f.IDRes = r.IDRes
        JOIN CUSTOMER c ON c.IDCustomer = @idCustomer
        WHERE f.IDFood = @idFood AND r.IDRes = @idRes";
            using var checkCmd = new MySqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@idCustomer", order.IDCustomer);
            checkCmd.Parameters.AddWithValue("@idFood", order.IDFood);
            checkCmd.Parameters.AddWithValue("@idRes", order.IDRes);
            using var reader = await checkCmd.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                return false;
            }
            await reader.CloseAsync();

            string insertQuery = @"
    INSERT INTO ORDERDETAIL (IDCustomer, IDFood, IDRes, Quantity, TotalPrice,FoodName,RestaurantName,Url_image)
    VALUES (@idCustomer, @idFood, @idRes, @quantity, @totalPrice, @foodName, @restaurantName, @url_image)";
            using var insertCmd = new MySqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@idCustomer", order.IDCustomer);
            insertCmd.Parameters.AddWithValue("@idFood", order.IDFood);
            insertCmd.Parameters.AddWithValue("@idRes", order.IDRes);
            insertCmd.Parameters.AddWithValue("@quantity", order.Quantity);
            insertCmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
              insertCmd.Parameters.AddWithValue("@foodName", order.FoodName);
                insertCmd.Parameters.AddWithValue("@restaurantName", order.RestaurantName);
                insertCmd.Parameters.AddWithValue("@url_image", order.Url_image);
            var result = await insertCmd.ExecuteNonQueryAsync();
            return result > 0;
        }
        public async Task<bool> DeleteOrderDetail(int IDOrder)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            string query = "DELETE FROM ORDERDETAIL WHERE IDOrder = @idOrder";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idOrder", IDOrder);

            int affectedRows = await cmd.ExecuteNonQueryAsync();
            return affectedRows > 0;
        }
        public async Task<List<OrderDetail>> GetOrderDetails(string ID)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            string query = @"
        SELECT od.IDOrder, od.IDCustomer, od.IDFood, od.IDRes, od.Quantity, od.TotalPrice, od.FoodName, od.RestaurantName,f.Url_image
                FROM ORDERDETAIL od
                JOIN FOOD f ON od.IDFood = f.IDFood
                JOIN RESTAURANT r ON od.IDRes = r.IDRes
                WHERE od.IDCustomer = @idCustomer";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idCustomer",ID);
                using var reader = await cmd.ExecuteReaderAsync();
                var orderDetails = new List<OrderDetail>();
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
                         orderDetails.Add(orderDetail);
                }
                return orderDetails;

       }
}
}
