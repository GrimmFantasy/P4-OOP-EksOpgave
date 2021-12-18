namespace EksOP
{
    public class Product 
    {
        private static int _prevID= 1;
        private int _id;
        private string _name;
        public int Id 
        { 
            get => _id;
            set 
            {
                if (value <= 0) { return; }
                else { _id = value; }
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
        public Product(string name, decimal price, int stat, int? id = null) 
        {
            Name = name;
            Price = price;
            if (stat == 0) { State = ProStat.inActive; }
            else{ State = ProStat.Active; }
            if (id != null && _prevID <= id) 
            {
                Id = id.Value;
                _prevID = id.Value;
                
            }
            else {
                Id = ++_prevID;
            }
           

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