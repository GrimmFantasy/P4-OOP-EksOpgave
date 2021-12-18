
namespace EksOP
{
    public interface IStregSystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        List<Transaction> Log { get; set; }
        List<Product> Products { get; set; }
        List<User> Users { get; set; }

        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetActiveProductById(int id);
        Product GetProductById(int id);
        List<Transaction> GetTransactions(User user, int count);
        User GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string userName);
        void ReadProduct();
        void ReadUsers();
        void WriteLog();
    }
}