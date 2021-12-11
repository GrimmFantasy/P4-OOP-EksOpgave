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
            Start();
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine("command not found");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"Oops, {errorString} occoud");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"You have insufficient cash for {product.Name}, you are missing {user.Balance-product.Price},-");
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
            Console.WriteLine($"{transaction.User.UserName} brought {transaction.Product.Name}");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"User: {user.UserName}");
            Console.WriteLine($"Balance: {user.Balance}");
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
