﻿namespace PBL3.Models
{
    public class Food
    {
        private static string ID = "0";
        private string Name;
        private int Price;
        private string IDRes;
        public string getIDRes()
        {
            return this.IDRes;
        }
        public void setIDRes(string IDRes)
        {
            this.IDRes = IDRes;
        }

        public static string getID()
        {
            return ID;
        }
        public static void setID()
        {
            int cc = Convert.ToInt32(ID);
            cc++;
            ID = cc.ToString();
        }

        public string getName()
        {
            return this.Name;
        }
        public void setName()
        {
            this.Name = Name;
        }
        public int getPrice()
        {
            return this.Price;
        }
        public void setPrice()
        {
            this.Price = Price;
        }
    }
}
