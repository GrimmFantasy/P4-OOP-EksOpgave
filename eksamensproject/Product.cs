namespace EksOP
{
    public class Product 
    {
        private static int prevId = 0;
        private int _id;
        private string _name;
        public int Id 
        { 
            get => _id;
            set 
            {
                if (Id >= 0) { throw new Exception($"{value} has to be 1 or above"); }
                _id = value;
            } 
        }
        public string Name 
        { 
            get => _name;
            set 
            {
                if (value == null) { throw new NullReferenceException($"{value} is null"); }
                _name = value;
            } 
        }
        public decimal Price { get; set; }
        public ProStat State { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
        public Product(string name, decimal price, bool onCredit) 
        {
            prevId++;
            Name = name;
            Price = price;
            CanBeBoughtOnCredit = onCredit;
            Price = price;
            Id = prevId;
        }
        public override string ToString() 
        { 
            return Id.ToString() + " " + Name + " " + Price.ToString();
        }
    }
    [Flags]
    public enum ProStat
    {
        inActive = 0,
        Active = 1
    }
}