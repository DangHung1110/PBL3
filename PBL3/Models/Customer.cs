using System.Text.Json.Serialization;

namespace PBL3.Models
{
    public class Customer
    {   public string IDCustomer { get; set; } = Guid.NewGuid().ToString(); 
public string Name { get; set; }
public string Address { get; set; }
   public string Phone { get; set; }
public string Pass { get; set; }
public string Role { get; set; } = "Customer"; 

        public Customer(string name, string address, string phone, string pass, string role)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Pass = pass;
            Role = role;
        }
     public Customer(string name, string address, string phone, string pass)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Pass = pass;
        }

        public Customer() { }}
}
