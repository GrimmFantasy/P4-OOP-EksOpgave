namespace EksOP
{
    public class StregsystemUI : IStregsystemUI
    {
        private StregSystem _system { get; set; }
        public StregsystemUI(StregSystem streg)
        {
            _system = streg;
        }

        public void Close()
        {
            Console.Clear();
            _system.WriteLog();
            Environment.Exit(0);
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"{adminCommand} command not found");
        }

        public void DisplayNotProductID(string errorString)
        {
            Console.WriteLine($"The \"{errorString}\" is not a number.");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"You have insufficient cash for {product.Name}, you are missing {product.Price-user.Balance},-");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Porduct id: {product} does not exist within the system");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"{command} is too much, i can't handle it");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
           Console.WriteLine($"{transaction.User.UserName} brought {transaction.Product.Name}");
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.User.UserName} brought {count} {transaction.Product.Name}");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user.ToString());
            Console.WriteLine($"User: {user.UserName}");
            Console.WriteLine($"Balance: {user.Balance}");
            if (user.Balance <= 50) 
            {
                Console.WriteLine("Low cash balacen!");
            }
            Console.WriteLine("10 Newst transactions");

            foreach (Transaction t in _system.GetTransactions(user, 10)) 
            {
                Console.WriteLine(t.ToString());
            }
            Console.WriteLine();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"username {username} not found");
        }
        public void DisplayProduct() 
        { 
            foreach (Product p in _system.ActiveProducts)
            { 
                Console.WriteLine(p.ToString());
            }
        }
        public void Start()
        {
            Console.Clear();
            Console.Write("Input Command: ");
            

        }
    }
}
