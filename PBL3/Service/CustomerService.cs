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
    INSERT INTO ORDERDETAIL (IDCustomer, IDFood, IDRes, Quantity, TotalPrice)
    VALUES (@idCustomer, @idFood, @idRes, @quantity, @totalPrice)";
    using var insertCmd = new MySqlCommand(insertQuery,conn);
insertCmd.Parameters.AddWithValue("@idCustomer", order.IDCustomer);
insertCmd.Parameters.AddWithValue("@idFood", order.IDFood);
insertCmd.Parameters.AddWithValue("@idRes", order.IDRes);
insertCmd.Parameters.AddWithValue("@quantity", order.Quantity);
insertCmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
var result = await insertCmd.ExecuteNonQueryAsync();
            return result > 0; }
            public async Task<bool> DeleteOrderDetail(int idOrder)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            string query = "DELETE FROM ORDERDETAIL WHERE IDOrder = @idOrder";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idOrder", idOrder);

            int affectedRows = await cmd.ExecuteNonQueryAsync();
            return affectedRows > 0;
        }
    }
}
