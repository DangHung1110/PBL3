﻿using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PBL3.Models;

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
            return result?.ToString(); // Trả về ID nếu tồn tại, null nếu không
        }
        public int SignUp([FromBody] Customer customer)
        {
            string IDCustomer, Name, Address, Phone, Pass;
            Customer.SetID();
            IDCustomer = Customer.GetID();
            Name = customer.GetName();
            Address = customer.GetAddress();
            Phone = customer.GetPhone();
            Pass = customer.GetPass();
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM CUSTOMER WHERE NAME=@Name", conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            var Check = Convert.ToInt32(cmd.ExecuteScalar());

            if(Check>0)
            {
                return 1;
            }
            else
            {
                var cmd2 = new MySqlCommand("INSERT INTO CUSTOMER (IDCustomer,Name,Address,Phone,Pass) VALUES(@IDCustomer,@Name,@Address,@Phone,@Pass)", conn);
                cmd2.Parameters.AddWithValue("@IDCustomer", IDCustomer);
                cmd2.Parameters.AddWithValue("@Name", Name);
                cmd2.Parameters.AddWithValue("@Address",Address);
                cmd2.Parameters.AddWithValue("@Phone",Phone);
                cmd2.Parameters.AddWithValue("@Pass", Pass);
                int rowaffected = cmd2.ExecuteNonQuery();
                if(rowaffected>0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }


        }
        public int Signup2([FromBody] Restaurant res)
        {
            string IDRes, Name, Address, Phone, Pass, img_url;
            Restaurant.SetID();
            IDRes = Restaurant.GetID();
            Name = res.GetName();
            Address = res.GetAddress();
            Phone = res.GetPhone();
            Pass = res.GetPass();
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM RESTAURANT WHERE Name=@Name", conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            var Check = Convert.ToInt32(cmd.ExecuteScalar());
            if (Check > 0)
            {
                return 1;
            }
            else
            {
                var cmd2 = new MySqlCommand("INSERT INTO RESTAURANT (IDRes,Name,Address,Phone,Pass) VALUES(@IDRes,@Name,@Address,@Phone,@Pass)", conn);
                cmd2.Parameters.AddWithValue("@IDRes", IDRes);
                cmd2.Parameters.AddWithValue("@Name", Name);
                cmd2.Parameters.AddWithValue("@Address", Address);
                cmd2.Parameters.AddWithValue("@Phone", Phone);
                cmd2.Parameters.AddWithValue("@Pass", Pass);
                int rowaffected = cmd2.ExecuteNonQuery();
                if (rowaffected > 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
