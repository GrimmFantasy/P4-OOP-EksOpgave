namespace EksOP
{
    public class BuyTransaction : Transaction
    {
        public Product Product { get; set; }
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
            if (!Product.CanBeBoughtOnCredit && User.Balance < Amount && Product.State == ProStat.inActive) { throw new Exception($"{User.UserName} has Insufficient Credits to buy {Product.Name}"); }
            User.Balance = User.Balance - Amount;
        }
    }
}