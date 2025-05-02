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
        {   try{
            using var conn = GetConnection();
            await conn.OpenAsync();
            
            DateTime datetime=TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
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
         await reader.DisposeAsync();
            if(order.Status_Restaurant=="pending")
            {string insertQuery = @"
    INSERT INTO ORDERDETAIL (IDCustomer, IDFood, IDRes, Quantity, TotalPrice,FoodName,RestaurantName,Url_image,Status_Restaurant,Status_User,OrderTime,OrderConfirmedTime)
    VALUES (@idCustomer, @idFood, @idRes, @quantity, @totalPrice, @foodName, @restaurantName, @url_image,@Status_Restaurant,@Status_User,@OrderTime,@OrderConfirmedTime)";
            using var insertCmd = new MySqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@idCustomer", order.IDCustomer);
            insertCmd.Parameters.AddWithValue("@idFood", order.IDFood);
            insertCmd.Parameters.AddWithValue("@idRes", order.IDRes);
            insertCmd.Parameters.AddWithValue("@quantity", order.Quantity);
            insertCmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
              insertCmd.Parameters.AddWithValue("@foodName", order.FoodName);
                insertCmd.Parameters.AddWithValue("@restaurantName", order.RestaurantName);
                insertCmd.Parameters.AddWithValue("@url_image", order.Url_image);
                insertCmd.Parameters.AddWithValue("@Status_Restaurant", order.Status_Restaurant);
                insertCmd.Parameters.AddWithValue("@Status_User", order.Status_User);
                insertCmd.Parameters.AddWithValue("@OrderTime", datetime);
                insertCmd.Parameters.AddWithValue("@OrderConfirmedTime", datetime);
            var result = await insertCmd.ExecuteNonQueryAsync();
            return result > 0;}
            else
            { string changequery="UPDATE ORDERDETAIL SET Status_Restaurant = @Status_Restaurant WHERE IDOrder= @IDOrder";
                using var changeCmd = new MySqlCommand(changequery, conn);
                changeCmd.Parameters.AddWithValue("@Status_Restaurant", order.Status_Restaurant);
                changeCmd.Parameters.AddWithValue("@IDOrder", order.IDOrder);
                var result = await changeCmd.ExecuteNonQueryAsync();
                return result > 0;
            }
        }
        catch(Exception ex)
        {
            // Xử lý lỗi nếu cần thiết
            Console.WriteLine(ex.Message);
            return false;
        }}
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
        SELECT od.IDOrder, od.IDCustomer, od.IDFood, od.IDRes, od.Quantity, od.TotalPrice, od.FoodName, od.RestaurantName,f.Url_image, od.Status_Restaurant,od.Status_User, od.OrderTime, od.OrderConfirmedTime
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
                            orderDetail.Status_Restaurant = reader.GetString("Status_Restaurant");
                    orderDetail.Status_User = reader.GetString("Status_User");
                    orderDetail.OrderTime = reader.GetDateTime("OrderTime");
                    orderDetail.OrderConfirmedTime = reader.GetDateTime("OrderConfirmedTime");
                         orderDetails.Add(orderDetail);
                }
                return orderDetails;

       }
       public async Task<int> GetProductCount(string IDCus)
{
    using var conn = GetConnection();
    await conn.OpenAsync();

    string query = "SELECT COUNT(*) FROM ORDERDETAIL WHERE IDCustomer = @idCustomer AND Status_User='pending'";

    using var cmd = new MySqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@idCustomer", IDCus);

    object result = await cmd.ExecuteScalarAsync();
    int count = Convert.ToInt32(result);

    return count;
}

}
}
