using System.Text.RegularExpressions;

namespace EksOP
{
    public class StregSystem : IStregSystem
    {
        public List<Transaction> Log { get; set; } = new List<Transaction>();
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public IEnumerable<Product> ActiveProducts { get{ return Products.Where(p => p.State == ProStat.Active); } }
        public StregSystem()
        {
            ReadUsers();
            ReadProduct();
        }
        public BuyTransaction BuyProduct(User user, Product product)
        {
            try
            {
                BuyTransaction transaction = new(product, user);
                transaction.Execute();
                Log.Add(transaction);
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            try
            {
                InsertCashTransaction transaction = new(user, amount);
                transaction.Execute();
                Log.Add(transaction);
                return transaction;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProductById(int id)
        {
            Product p = Products.Where(a => a.Id == id).FirstOrDefault();
            return p;
        }
        public Product GetActiveProductById(int id)
        {
            Product p = ActiveProducts.Where(a => a.Id == id).Single();
            return p;
        }
        public User GetUserByUsername(string userName)
        {
            string username = userName;
            User u = Users.Where(u => u.GetHashCode() == username.GetHashCode()).FirstOrDefault();
            if (u != null) { return u; }
            return null;
        }
        public void ReadUsers()
        {
            Users = new List<User>();
            foreach (var line in File.ReadLines(@"../../../users.csv"))
            {
                if (!line.StartsWith("id"))
                {
                    var array = line.Split(',');
                    User u = new(array[1], array[2], array[5], array[3]);
                    try
                    {
                        decimal balance = Convert.ToDecimal(array[4]);
                        int id = Convert.ToInt32(array[0]);
                        u.Id = id;
                        u.Balance = balance;

                    }
                    catch (Exception ex) { Console.WriteLine("Cloud not convert" + ex); }

                    Users.Add(u);
                }

            }
        }
        public void ReadProduct()
        {
            Products = new List<Product>();
            decimal price;
            int id;
            int state;
            foreach (var line in File.ReadLines(@"../../../products.csv"))
            {
                if (!line.StartsWith("id"))
                {

                    var array = Regex.Replace(line, "<.*?>", String.Empty).Split(';');
                    try
                    {
                        price = Convert.ToDecimal(array[2]);
                        id = Convert.ToInt32(array[0]);
                        state = Convert.ToInt32(array[3]);
                    }
                    catch (Exception ex) { throw ex; }
                    if (array[4] != "" || array[4] != " ")
                    {
                        Product p = new(array[1], price, state, id);
                        Products.Add(p);
                    }
                    else
                    {
                        DateTime diactDate;
                        try
                        {
                            diactDate = Convert.ToDateTime(array[4]);
                            SeasonalProduct p = new(id, array[1], price, state, diactDate);
                            Products.Add(p);
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }
                }

            }


        }
        public List<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> UserTanscations = Log.Where(l => l.User.GetHashCode == user.GetHashCode).Take(count).ToList();
            UserTanscations.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return UserTanscations;
        }
        
        public User GetUsers(Func<User, bool> predicate)
        {
            User user = Users.Where(predicate).FirstOrDefault();
            return user == null ? throw new Exception() : user;
        }
        public void WriteLog()
        {
            List<string> logs = new List<string>();

            foreach (var t in Log)
            {
                logs.Add(t.ToString());


            }


            File.WriteAllLines(@"../../../TransLog.csv", logs);
        }
    }

}