namespace EksOP
{
    public class StregSystem 
    {
        public List<string> Log { get; set; }
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
        public void ReadUsers() 
        {
            Users = new List<User>();
            using (var reader = new StreamReader(@"C:\Users\capse\Documents\GitHub\EksOpgave\eksamensproject\users.csv"))
            {
               
                while (!reader.EndOfStream)
                {
                    List<string> lines = reader.ReadLine().Split().ToList();
                    foreach (var line in lines) 
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
            }
        }
        public void ReadProduct() 
        { 
            Products = new List<Product>();
            using (var reader = new StreamReader(@"C:\Users\capse\Documents\GitHub\EksOpgave\eksamensproject\products.csv"))
            {

                while (!reader.EndOfStream)
                {
                    List<string> lines = reader.ReadLine().Split().ToList();
                    decimal price;
                    int id;
                    int state;
                    foreach (var line in lines)
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
                                Product p = new(array[1], price, state);
                                p.Id = id;
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
                                SeasonalProduct p = new(array[1], price, state, diactDate);
                                p.Id=id;
                                Products.Add(p);
                            }
                        }

                    }
                }
            }
        }
    }

}