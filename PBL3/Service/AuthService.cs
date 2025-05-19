using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PBL3.Models;
using PBL3.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PBL3.Service
{
    public class AuthService
    {
        private readonly string _uploadFolder = "wwwroot/Uploads";
        private readonly string baseurl = "http://localhost:5299/Uploads/";
        private readonly IConfiguration _iconfiguration;
        public AuthService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_iconfiguration.GetConnectionString("DefaultConnection"));
        }
        public string ImageDeal(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(_uploadFolder, filename);
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }

            string urldb = baseurl + filename;
            return urldb;
        }



        public Object Login(string name, string pass)
        {
            using var conn = GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT IDCustomer, Name, Address, Phone FROM CUSTOMER WHERE Name=@Name AND Pass=@Pass", conn);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Pass", pass);
            Console.WriteLine(name);
            Console.WriteLine(pass);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Customer x = new Customer();

                x.IDCustomer = reader.GetString("IDCustomer");
                x.Name = reader.GetString("Name");
                x.Address = reader.GetString("Address");
                x.Phone = reader.GetString("Phone");
                x.Role = "Customer";
                Console.WriteLine(reader.GetString("Phone"));
                Console.WriteLine(reader.GetString("Address"));


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
            reader2.Close();
            var tt = new MySqlCommand("SELECT IDGrab,Name,Phone FROM Grab WHERE Name=@Name AND Pass=@Pass", conn);
            tt.Parameters.AddWithValue("@Name", name);
            tt.Parameters.AddWithValue("@Pass", pass);
            using var reader3 = tt.ExecuteReader();
            if (reader3.Read())
            {
                var x = new Grab();
                x.IDGrab = reader3.GetInt32("IDGrab");
                x.Name = reader3.GetString("Name");
                x.Phone = reader3.GetString("Phone");

                x.Role = "Grab";
                return x;


            }
            if (name == "Dương Tùng Bách Cooporation" && pass == "68066666")
            {
                Object x = new
                {
                    Role = "Admin",
                    Name = "Dương Tùng Bách Cooporation",
                    Phone = "68066666",
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


        public int Signup2([FromBody] RESTAURANTWAITDTO res)
        {

            if (res == null)
            {
                return 1;
            }




            string Name = res.Name;
            Console.WriteLine(Name);
            string Address = res.Address;
            string Phone = res.Phone;
            string Pass = res.Pass;
            string Url_Image = res.Url_Image;
            string Url_Image2 = res.Url_Image2;
            string Url_Image3 = res.Url_Image3;
            Console.WriteLine(Url_Image);
            Console.WriteLine(Url_Image2);
            Console.WriteLine(Url_Image3);



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

                var cmd2 = new MySqlCommand("INSERT INTO  RESTAURANTWAIT (Name, Address, Phone, Pass, Url_Image,Url_Image2,Url_Image3) VALUES( @Name, @Address, @Phone, @Pass, @Url_Image,@Url_Image2,@Url_Image3)", conn);
                cmd2.Parameters.AddWithValue("@Url_Image", Url_Image);
                cmd2.Parameters.AddWithValue("@Url_Image2", Url_Image2);
                cmd2.Parameters.AddWithValue("@Url_Image3", Url_Image3);
                cmd2.Parameters.AddWithValue("@Name", Name);
                cmd2.Parameters.AddWithValue("@Address", Address);
                cmd2.Parameters.AddWithValue("@Phone", Phone);
                cmd2.Parameters.AddWithValue("@Pass", Pass);
                cmd2.Parameters.AddWithValue("@Status", Pass);

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

                    Console.WriteLine(ex.ToString());
                    return 2;
                }
            }
        }
        public int Signup3([FromBody] Restaurant res)
        {

            if (res == null)
            {
                return 1;
            }





            string Address = res.Address;
            string Phone = res.Phone;
            string Pass = res.Pass;
            string Url_Image = res.Url_Image;
            string Name = res.Name;
            string Role = "Restaurant";
            Restaurant.SetNextID();
            String IDRes = Restaurant.ID;





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

                var cmd2 = new MySqlCommand("INSERT INTO  RESTAURANT (IDRes,Name, Address, Phone, Pass, Url_Image,Role) VALUES(@IDRes,@Name, @Address, @Phone, @Pass, @Url_Image,@Role)", conn);
                cmd2.Parameters.AddWithValue("@Url_Image", Url_Image);
      cmd2.Parameters.AddWithValue("@IDRes",IDRes);
                cmd2.Parameters.AddWithValue("@Name", Name);
                cmd2.Parameters.AddWithValue("@Address", Address);
                cmd2.Parameters.AddWithValue("@Phone", Phone);

                cmd2.Parameters.AddWithValue("@Pass", Pass);
                cmd2.Parameters.AddWithValue("@Role", Role);

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

                    Console.WriteLine(ex.ToString());
                    return 2;
                }
            }
        }
    }
}
