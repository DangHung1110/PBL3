namespace PBL3.Models
{
    public class Restaurant
    {
        private static string ID="0";
        private string Name;
        private string Address;
        private string Phone;
        private string Pass;
        private List<Food>Menu;
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
        public string GetName()
        {
            return this.Name;
        }
        public void SetName(string Name)
        {
            this.Name= Name;
        }
        public string GetAddress()
        {
            return this.Address;
        }
        public void SetAddress(string Address)
        {
            this.Address=Address;
        }
        public string GetPhone()
        {
            return this.Phone;
        }
        public void SetPhone(string Phone)
        {
            this.Phone = Phone;
        }
        public string GetPass()
        {
            return this.Pass;
        }
        public void SetPass(string Pass)
        {
            this.Pass = Pass;
        }
        public List<Food>getFoodsList()
        {
            return this.Menu;
        }
    }
}
