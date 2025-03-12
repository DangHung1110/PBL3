namespace PBL3.Models
{
    public class Restaurant
    {
        private string ID;
        private string Name;
        private string Address;
        private string Phone;
        private string Pass;
        public string GetID()
        {
            return this.ID;
        }
        public void SetID(string ID)
        {
            this.ID = ID;
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
    }
}
