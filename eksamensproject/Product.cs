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
                if (Id > 0) { _id = value; }
                
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
        public Product(string name, decimal price, int stat) 
        {
            prevId++;
            Name = name;
            Price = price;
            if (stat == 1) { State = ProStat.Active; }
            else{ State = ProStat.inActive; }
            Id = prevId;
        }
        public override string ToString() 
        { 
            return Id.ToString() + " " + Name + " " + Price.ToString();
        }
    }
    public enum ProStat
    {
        inActive = 0,
        Active = 1
    }
}