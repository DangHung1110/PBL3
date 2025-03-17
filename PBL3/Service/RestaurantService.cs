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
        
       

    }
}
