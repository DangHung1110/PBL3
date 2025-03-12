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
        public List<Customer>GetAll()
        {
            List<Customer> DS = new List<Customer>();
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT*FROM Customer", conn);
            using var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Customer x = new Customer();
                x.SetID(reader.GetString("ID"));
                x.SetName(reader.GetString("Name"));
                x.SetAddress(reader.GetString("Address"));
                x.SetPhone(reader.GetString("Phone"));
                x.SetPass(reader.GetString("Pass"));

                DS.Add(x);
            }
            return DS;
        }
        public String Login(String Name,String Pass)
        {
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT IDCustomer FROM CUSTOMER WHERE NAME=@Name AND PASS=@Pass", conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            var Result = cmd.ExecuteScalar();
            if(Result.ToString()!=null)
            {
                return Result.ToString();
            }
            else
            {
                return null;
            }


        }
    }
}
