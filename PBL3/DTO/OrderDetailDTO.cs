public class OrderDetailDTO
{
    // KHÔNG khai báo IDOrder ở đây
    public int? IDOrder { get; set; }
    public string IDCustomer { get; set; }
    public string IDFood { get; set; }
    public string IDRes { get; set; }
    public string Url_Image { get; set; }
    public string FoodName { get; set; }
    public string RestaurantName { get; set; }
    public int Quantity { get; set; }
    public int TotalPrice { get; set; }
    public string Status_Restaurant { get; set; }
}
