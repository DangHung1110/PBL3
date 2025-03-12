namespace PBL3.Models
{
    public class DeliveryDriver
    {
        private string IDDriver;
        private string Name;
        
        private string Phone;
        private string Status;
        private string Pass;
        public string GetID()
        {
            return this.IDDriver;
        }
        public void SetID(string ID)
        {
            this.IDDriver = ID;
        }
        public string GetName()
        {
            return this.Name;
        }
        public void SetName(string Name)
        {
            this.Name = Name;
        }
        public string GetStatus()
        {
            return this.Status;
        }
        public void SetStatus(string Address)
        {
            this.Status = Address;
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
