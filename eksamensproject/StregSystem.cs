namespace EksOP
{
    public class StregSystem 
    {
        List<string> Log { get; set; }
        List<User> Users { get; set; }
        IEnumerable<Product> ActiveProducts { get; set; } 
        public StregSystem() { }
        public string BuyProduct(User user, Product product) 
        {
            BuyTransaction transaction = new BuyTransaction(product, user);
            transaction.Execute();
            return transaction.ToString();
        }
        public string AddCreditsToAccount(User user, decimal amount) 
        { 
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            transaction.Execute();
            return transaction.ToString();
        }
        public void AddCreditsToAccount(InsertCashTransaction insert) 
        {
            try
            {
                insert.Execute();
                Log.Add(insert.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void BuyProduct(BuyTransaction buy)
        {
            try
            {
                buy.Execute();
                Log.Add(buy.ToString());
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }
        public Product GetProductById(int id) 
        {
            Product p = ActiveProducts.Where(a => a.Id == id).Single();
            return p;
        }
        public User GetUserByUsername(string username) 
        {
            return Users.Where(u => u.GetHashCode() == username.GetHashCode()).Single();
        }

    }

}