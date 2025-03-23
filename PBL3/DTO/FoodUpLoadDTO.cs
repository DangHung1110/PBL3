namespace PBL3.DTO
{
    public class FoodUpLoadDTO
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string IDRes { get; set; }
        public IFormFile Url_Image { get; set; }
    }
}
