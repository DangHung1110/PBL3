namespace PBL3.Models
{
    public class Thongke
    {
        public int? ID{ get; set; }
        public string IDRes {get;set;}
        public DateTime CusConfirmedTime {get;set;}
    
        public string IDCustomer {get;set;}
    public DateTime OrderTime {get;set;}
    public DateTime OrderConfirmedTime{get;set;}
    public int Quantity {get;set;}
    public string RestaurantName {get;set;}
    public string FoodName {get;set;}
    public string Url_Image {get;set;}
      public int Doanhso {get;set;}
        
    }
}
