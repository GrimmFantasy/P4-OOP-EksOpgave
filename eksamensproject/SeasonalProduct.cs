namespace EksOP
{
    public class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }
        public SeasonalProduct(int id, string name, decimal price, int state, DateTime end) : base(name, price, state, id)
        {
           SeasonEndDate = end;

        }
    }

}