namespace PBL3.Models
{
    public class OrderDetail
    {
        public int? IDOrder { get; set; }
        public string IDFood { get; set; }
        public string IDCustomer { get; set; }
        public int TotalPrice {get;set;}
        public int Quantity {get;set;}
        public string IDRes { get; set; }
    }
}