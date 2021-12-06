namespace EksOP
{
    public class StregSystem 
    {
        public List<Transaction> Log { get; set; } = new List<Transaction>();
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public IEnumerable<Product> ActiveProducts { get; set; } 
        public StregSystem() { }
        public string BuyProduct(User user, Product product) 
        {
            BuyTransaction transaction = new(product, user);
            transaction.Execute();
            return transaction.ToString();
        }
        public string AddCreditsToAccount(User user, decimal amount) 
        { 
            InsertCashTransaction transaction = new(user, amount);
            transaction.Execute();
            return transaction.ToString();
        }
        public void AddCreditsToAccount(InsertCashTransaction insert) 
        {
            try
            {
                insert.Execute();
                Log.Add(insert);
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
                Log.Add(buy);
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
        public void ReadUsers() 
        {
            Users = new List<User>();
            foreach (var line in File.ReadLines(@"C:\Users\capse\Documents\GitHub\EksOpgave\eksamensproject\users.csv")) 
            {
                if (!line.StartsWith("id")) 
                { 
                var array = line.Split(',');
                User u = new(array[1], array[2], array[5], array[3]);
                try
                {
                    decimal balance = Convert.ToDecimal(array[4]);
                    int id = Convert.ToInt32(array[0]);
                    u.Id=id;
                    u.Balance=balance;

                }
                catch (Exception ex) { throw ex; }

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
            foreach (var line in File.ReadLines(@"C:\Users\capse\Documents\GitHub\EksOpgave\eksamensproject\products.csv"))
            {
                if (!line.StartsWith("id"))
                {
                    var array = line.Split(';');
                    try
                    {
                        price = Convert.ToDecimal(array[2]);
                        id = Convert.ToInt32(array[0]);
                        state = Convert.ToInt32(array[3]);
                    }
                    catch (Exception ex) { throw ex; }
                    if (array[4] != "" || array[4] != " ")
                    {
                        Product p = new(id, array[1], price, state);
                        Products.Add(p);
                    }
                    else 
                    {
                        DateTime diactDate;
                        try 
                        {
                            diactDate = Convert.ToDateTime(array[4]);
                                
                        }
                        catch(Exception ex) { throw ex; }
                        SeasonalProduct p = new(id, array[1], price, state, diactDate);
                        Products.Add(p);
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
        public void AktiveProducts() 
        {
            ActiveProducts = Products.Where(p=> p.State == ProStat.Active);
        }
        public User GetUser(Func<User, bool> predicate)
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


            File.WriteAllLines(@"C:\Users\capse\Documents\GitHub\EksOpgave\eksamensproject\TransLog.csv", logs);
        }
    }

}