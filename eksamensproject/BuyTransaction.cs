namespace EksOP
{
    public class BuyTransaction : Transaction
    {
        public Product Product { get; }
        public BuyTransaction(Product product, User user) : base(user, product.Price)
        {
            Product = product;
        }
        public override string ToString() 
        {
            return Id.ToString() + ";" + User.UserName.ToString() + ";" + Product.Name + ";" + Amount + ";" + Date.ToString();
        }

        public void Execute() 
        {
            if (!Product.CanBeBoughtOnCredit && User.Balance < Amount) { throw new Exception($"{User.UserName} has Insufficient Credits to buy {Product.Name}."); }
            else if(Product.State == ProStat.inActive) { throw new Exception($"{Product.Name} can not be brought at this time."); }
            User.Balance = User.Balance - Amount;
        }
    }
}