namespace EksOP
{
    internal class StregsystemUI : IStregsystemUI
    {
        private StregSystem _system { get; set; }
        public StregsystemUI(StregSystem streg)
        {
            _system = streg;
        }

        public void Close()
        {
            
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine("Admin command not found");
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Porduct id: {product} does not exist within the system");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"username {username} not found");
            Start();
        }
        public void DisplayProduct() 
        { 
            Console.Clear();
            foreach (Product p in _system.ActiveProducts)
            { 
                Console.WriteLine(p.ToString());
            }
        }
        public void Start()
        {
            Console.Write("Input Username: ");
            string userName = Console.ReadLine();
            if (_system.GetUserByUsername(userName)==null) { DisplayUserNotFound(userName); }
            DisplayProduct();
            Console.Write("Enter Product id: ");
            string productids = Console.ReadLine();
            List<string> ids = productids.Split('+').ToList<string>();
            foreach (string id in ids) 
            {
                try 
                {
                    _system.BuyProduct(_system.GetUserByUsername(userName), _system.GetProductById(Convert.ToInt32(id)));
                                        
                }
                catch { DisplayProductNotFound(id); }
            }

        }
    }
}
