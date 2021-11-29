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
            return "Transktion:" + User.UserName.ToString() + " brought " +" "+ Product.Name + Amount + " on the " + Date.ToString() + ". Id: " + Id.ToString();
        }

        public void Execute() 
        {
            if (!Product.CanBeBoughtOnCredit && User.Balance < Amount) { throw new Exception($"{User.UserName} has Insufficient Credits to buy {Product.Name}"); }
            User.Balance = User.Balance - Amount;
        }
    }
}