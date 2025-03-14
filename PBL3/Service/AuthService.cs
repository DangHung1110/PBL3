using MySqlConnector;

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
        public string Login(String Name, String Pass)
        {
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT IDCustomer FROM CUSTOMER WHERE NAME=@Name AND PASS=@Pass", conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            var Result = cmd.ExecuteScalar();
            if (Result.ToString() != null)
            {
                return Result.ToString();
            }
            else
            {
                return null;
            }



        }
        public string Login2(string Name, string Pass)
        {
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT IDRes FROM RESTAURANT WHERE Name=@Name AND Pass=@Pass ", conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            var result = cmd.ExecuteScalar();
            if (result.ToString() != null)
            {
                return result.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
