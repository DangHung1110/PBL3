using System.Text.Json.Serialization;

namespace PBL3.Models
{
    public class Customer
    {   
        [JsonInclude]
        [JsonPropertyName("IDCustomer")]
        public string ID { get; set; } = Guid.NewGuid().ToString(); // Tạo ID duy nhất
        
        [JsonInclude]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonInclude]
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonInclude]
        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        [JsonInclude]
        [JsonPropertyName("Pass")]
        public string Pass { get; set; }

        // Constructor để dễ tạo đối tượng
        public Customer(string name, string address, string phone, string pass)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Pass = pass;
        }

        // Constructor mặc định
        public Customer() { }
    }
}
