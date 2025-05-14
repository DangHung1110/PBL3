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
        public string FoodName { get; set; }
        public string RestaurantName { get; set; }
        public string Url_image {get;set;}
        public string Status_Restaurant {get;set;}
        public string Status_User {get;set;}
        public DateTime OrderTime {get;set;}
        public DateTime OrderConfirmedTime {get;set;}
        public int IDGrab {get;set;}
        public string Phone {get;set;}
        public string Address {get;set;}
        }
}