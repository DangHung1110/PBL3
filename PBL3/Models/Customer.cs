using System.Text.Json.Serialization;

namespace PBL3.Models
{
    public class Customer
    {   
        [JsonInclude]
        [JsonPropertyName("IDCustomer")]
        private static string ID="0";
        
           
        
        public static string GetID()
        {
            return ID;
        }

        public static void SetID()
        {
            int cc = Convert.ToInt32(ID);
            cc++;
            ID = cc.ToString();
            
        }
        [JsonInclude]
        [JsonPropertyName("Name")]
        private string Name;
        public string GetName()
        {
            return this.Name;
        }
        public void SetName(string Name)
        {
            this.Name = Name;
        }
        [JsonInclude]
        [JsonPropertyName("Address")]
        private string Address;
        public string GetAddress()
        {
            return this.Address;
        }
        public void SetAddress(string Address)
        {
            this.Address = Address;
        }
        [JsonInclude]
        [JsonPropertyName("Phone")]
        private string Phone;
        public string GetPhone()
        {
            return this.Phone;
        }
        public void SetPhone(string Phone)
        {
            this.Phone = Phone;
        }
        [JsonInclude]
        [JsonPropertyName("Pass")]
        private string Pass;
        public string GetPass()
        {
            return this.Pass;
        }
        public void SetPass(string Pass)
        {
            this.Pass = Pass;
        }






    }
}
