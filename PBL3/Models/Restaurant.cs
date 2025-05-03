namespace PBL3.Models
{
    public class Restaurant
    {
        private static int _idCounter = 0;

        // Trả về ID có dạng "RES001", "RES002"...
        public static string ID => "RES" + (_idCounter + 1).ToString("D3");

        public static void SetNextID()
        {
            _idCounter++;
        }
        public string IDRes { get; set; } = ID; // Tạo ID duy nhất
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Pass { get; set; }
        public List<Food> Menu { get; set; }
        public string Url_Image { get; set; }
        public string Role {get;set;}= "Restaurant";

        public Restaurant()
        {
            Menu = new List<Food>();
        }

        public List<Food> GetFoodsList()
        {
            return Menu;
        }
    }
}
