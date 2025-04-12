using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PBL3.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PBL3.Service
{
    public class AuthService
    {
        private readonly IConfiguration _iconfiguration;
        public AuthService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_iconfiguration.GetConnectionString("DefaultConnection"));
        }
        // public string Login(String Name, String Pass)
        // {
        //     using var conn = GetConnection();
        //     conn.Open();
        //     var cmd = new MySqlCommand("SELECT IDCustomer FROM CUSTOMER WHERE NAME=@Name AND PASS=@Pass", conn);
        //     cmd.Parameters.AddWithValue("@Name", Name);
        //     cmd.Parameters.AddWithValue("@Pass", Pass);
        //     var Result = cmd.ExecuteScalar();
        //     if (Result != null)
        //     {
        //         return Result.ToString();
        //     }
        //     else
        //     {
        //         return null;
        //     }



        // }
        // public string Login2(string Name, string Pass)
        // {
        //     using var conn = GetConnection();
        //     conn.Open();
        //     var cmd = new MySqlCommand("SELECT IDRes FROM RESTAURANT WHERE Name=@Name AND Pass=@Pass ", conn);
        //     cmd.Parameters.AddWithValue("@Name", Name);
        //     cmd.Parameters.AddWithValue("@Pass", Pass);
        //     var result = cmd.ExecuteScalar();
        //     if (result != null)
        //     {
        //         return result.ToString();
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }

        public string Login(string name, string pass, string role)
        {
            using var conn = GetConnection();
            conn.Open();

            // Chọn bảng tương ứng dựa trên role
            string query = role == "user"
                ? "SELECT IDCustomer FROM CUSTOMER WHERE Name=@Name AND Pass=@Pass"
                : "SELECT IDRes FROM RESTAURANT WHERE Name=@Name AND Pass=@Pass";

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Pass", pass);

            var result = cmd.ExecuteScalar();
            return JsonSerializer.Serialize(new
            {
                success = true,
                role = role,
                id = result?.ToString()
            });
            // Trả về ID nếu tồn tại, null nếu không
        }
        public int SignUp([FromBody] Customer customer)
        {
            using var conn = GetConnection();
            conn.Open();

            // Kiểm tra xem tên khách hàng đã tồn tại chưa
            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM CUSTOMER WHERE Name=@Name", conn);
            checkCmd.Parameters.AddWithValue("@Name", customer.Name);
            var check = Convert.ToInt32(checkCmd.ExecuteScalar());
            if (check > 0)
            {
                return 1;  // Tên đã tồn tại
            }

            // Sinh IDCustomer dạng CUS001, CUS002,...
            var idCmd = new MySqlCommand("SELECT IDCustomer FROM CUSTOMER WHERE IDCustomer LIKE 'CUS%' ORDER BY IDCustomer DESC LIMIT 1", conn);
            string newID = "CUS000";
            var lastIDObj = idCmd.ExecuteScalar();
            if (lastIDObj != null)
            {
                string lastID = lastIDObj.ToString();  // VD: "CUS005"
                int number = int.Parse(lastID.Substring(3));  // Lấy phần số: 5
                newID = "CUS" + (number + 1).ToString("D3");  // Tăng lên: "CUS006"
            }

            // Thêm khách hàng vào cơ sở dữ liệu với IDCustomer sinh ra
            var cmd = new MySqlCommand("INSERT INTO CUSTOMER (IDCustomer, Name, Address, Phone, Pass) VALUES(@IDCustomer, @Name, @Address, @Phone, @Pass)", conn);
            cmd.Parameters.AddWithValue("@IDCustomer", newID);
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@Phone", customer.Phone);
            cmd.Parameters.AddWithValue("@Pass", customer.Pass);

            try
            {
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 2;  // Lỗi hệ thống
            }
        }


        public int Signup2([FromBody] Restaurant res)
        {
            // Kiểm tra nếu restaurant null
            if (res == null)
            {
                return 1;  // Có thể trả về mã lỗi tùy chỉnh hoặc thông báo lỗi ở đây
            }

            Restaurant.SetNextID();
            // Lấy thông tin từ restaurant
            string IDRes = Restaurant.ID;  // ID đã được tạo tự động khi khởi tạo Restaurant
            string Name = res.Name;
            string Address = res.Address;
            string Phone = res.Phone;
            string Pass = res.Pass;
            string Url_Image = res.Url_Image;

            // Kiểm tra nhà hàng đã tồn tại trong cơ sở dữ liệu
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM RESTAURANT WHERE Name=@Name", conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            var Check = Convert.ToInt32(cmd.ExecuteScalar());

            if (Check > 0)
            {
                return 1;  // Nhà hàng đã tồn tại
            }
            else
            {
                // Thêm nhà hàng mới vào cơ sở dữ liệu
                var cmd2 = new MySqlCommand("INSERT INTO RESTAURANT (IDRes, Name, Address, Phone, Pass, Url_Image) VALUES(@IDRes, @Name, @Address, @Phone, @Pass, @Url_Image)", conn);
                cmd2.Parameters.AddWithValue("@Url_Image", Url_Image);
                cmd2.Parameters.AddWithValue("@IDRes", IDRes);
                cmd2.Parameters.AddWithValue("@Name", Name);
                cmd2.Parameters.AddWithValue("@Address", Address);
                cmd2.Parameters.AddWithValue("@Phone", Phone);
                cmd2.Parameters.AddWithValue("@Pass", Pass);

                try
                {
                    int rowAffected = cmd2.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        return 0;  // Đăng ký thành công
                    }
                    else
                    {
                        return 1;  // Lỗi khi thêm vào cơ sở dữ liệu
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi, có thể ghi log hoặc thông báo lỗi chi tiết
                    return 2;  // Mã lỗi cho lỗi hệ thống (database connection, etc.)
                }
            }
        }

    }
}
