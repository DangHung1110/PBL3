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
      

      public Object Login(string name, string pass)
{
    using var conn = GetConnection();
    conn.Open();

    var cmd = new MySqlCommand("SELECT IDCustomer, Name, Address, Phone FROM CUSTOMER WHERE Name=@Name AND Pass=@Pass", conn);
    cmd.Parameters.AddWithValue("@Name", name);
    cmd.Parameters.AddWithValue("@Pass", pass);
    
    using var reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        var x = new Customer
        {
            IDCustomer = reader.GetString("IDCustomer"),
            Name = reader.GetString("Name"),
            Address = reader.GetString("Address"),
            Phone = reader.GetString("Phone"),
            Role = "Customer"
        };
        return x;
    }

    reader.Close(); 

    var cc = new MySqlCommand("SELECT IDRes, Name, Address, Phone FROM RESTAURANT WHERE Name=@Name AND Pass=@Pass", conn);
    cc.Parameters.AddWithValue("@Name", name);
    cc.Parameters.AddWithValue("@Pass", pass);
    
    using var reader2 = cc.ExecuteReader();
    if (reader2.Read())
    {
        var x = new Restaurant
        {
            IDRes = reader2.GetString("IDRes"),
            Name = reader2.GetString("Name"),
            Address = reader2.GetString("Address"),
            Phone = reader2.GetString("Phone"),
            Role = "Restaurant"
        };
        return x;
    }

    return null;
}

        public int SignUp([FromBody] Customer customer)
        {
            using var conn = GetConnection();
            conn.Open();

        
            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM CUSTOMER WHERE Name=@Name", conn);
            checkCmd.Parameters.AddWithValue("@Name", customer.Name);
            var check = Convert.ToInt32(checkCmd.ExecuteScalar());
            if (check > 0)
            {
                return 1;  
            }

         
            var idCmd = new MySqlCommand("SELECT IDCustomer FROM CUSTOMER WHERE IDCustomer LIKE 'CUS%' ORDER BY IDCustomer DESC LIMIT 1", conn);
            string newID = "CUS000";
            var lastIDObj = idCmd.ExecuteScalar();
            if (lastIDObj != null)
            {
                string lastID = lastIDObj.ToString();  
                int number = int.Parse(lastID.Substring(3));  
                newID = "CUS" + (number + 1).ToString("D3");
            }

        
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
                return 2; 
            }
        }


        public int Signup2([FromBody] Restaurant res)
        {
           
            if (res == null)
            {
                return 1;  
            }

            Restaurant.SetNextID();
         
            string IDRes = Restaurant.ID;  
            string Name = res.Name;
            string Address = res.Address;
            string Phone = res.Phone;
            string Pass = res.Pass;
            string Url_Image = res.Url_Image;

       
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
                        return 0;  
                    }
                    else
                    {
                        return 1; 
                    }
                }
                catch (Exception ex)
                {
                    
                    return 2;  
                }
            }
        }
    }
}
