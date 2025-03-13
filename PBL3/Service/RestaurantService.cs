using MySqlConnector;
namespace PBL3.Service
{
    public class RestaurantService
    {
        private readonly IConfiguration _iconfiguration;
        public RestaurantService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_iconfiguration.GetConnectionString("DefaultConneciton"));
        }
        public string Login2(string Name,string Pass)
        {
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT IDRes FROM RESTAURANT WHERE Name=@Name AND Pass=@Pass ", conn);
            cmd.Parameters.AddWithValue("@Name",Name);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            var result = cmd.ExecuteScalar();
            if(result.ToString()!=null)
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
