namespace PBL3.DTO
{
    public class RESTAURANTWAITDTO
    {
       
        public int? IDRes { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Pass { get; set; }
      
        public string Url_Image { get; set; }
          public string Url_Image2{ get; set; }
            public string Url_Image3 { get; set; }
        public string Role { get; set; } = "Restaurant";

       
    }
}
