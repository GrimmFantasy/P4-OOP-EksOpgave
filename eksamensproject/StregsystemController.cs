namespace EksOP
{
    public class StregsystemController
    {
        public StregSystem stregSystem { get; set; }
        public IStregsystemUI stregsystemUI { get; set;}
        public User CurrentUser { get; set; }
        public StregsystemController(StregSystem streg, IStregsystemUI uI)
        {
            this.stregSystem = streg;
            this.stregsystemUI = uI;
        }
        public void Start() 
        {
            stregsystemUI.Start();
            string command = Console.ReadLine();
            ParseCommand(command);
        }
        public void restart() 
        {
            Thread.Sleep(5000);
            Start();
        }
        public void ParseCommand(string command) 
        {
            if (command.StartsWith(":"))
            {

                switch (command.Split(' ')[0]) 
                {
                    case ":q":
                    case ":quit":
                        quit(); 
                        break;
                    case ":activate":
                    case ":deactivate":
                        productChangeState(command);
                        break;
                    case ":crediton": 
                    case ":creditoff":
                        ProductCeditEdit(command);
                        break;
                    case ":addcredits":
                        Addcredits(command);
                        break;
                    default:
                        stregsystemUI.DisplayAdminCommandNotFoundMessage(command);
                        break;
                }

            }
            else
            { 
                buyPhase(command);
                restart();
            }
            
        }
        public void buyPhase(string command)
        {
            List<string> commands = command.Split().ToList<string>();
            CurrentUser = stregSystem.GetUserByUsername(commands[0]);
            if (CurrentUser == null) { stregsystemUI.DisplayUserNotFound(commands[0]); }
            else
            {
                stregsystemUI.DisplayUserInfo(CurrentUser);
                commands.RemoveAt(0);
                Product p;
                if (commands.Count < 1)
                {
                    stregsystemUI.DisplayProduct();
                    Console.WriteLine();
                    Console.Write("Enter product id(s):");
                    string productids = Console.ReadLine();
                    List<string> ids = productids.Split('+').ToList<string>();
                    
                    foreach (string id in ids)
                    {

                        p = stregSystem.GetActiveProductById(Convert.ToInt32(id));
                        if (p != null) 
                        { 
                            try
                            {
                                stregsystemUI.DisplayUserBuysProduct(stregSystem.BuyProduct(CurrentUser, p));
                            }
                            catch { stregsystemUI.DisplayInsufficientCash(CurrentUser, p); }
                        }
                        else { stregsystemUI.DisplayProductNotFound(id); }    
                    }
                    
                }
                else 
                {
                    string temp = commands.Last();
                    try
                    {
                        int tempId = Int32.Parse(commands.Last());
                        int count = Int32.Parse(commands.First());
                            
                        p = stregSystem.GetActiveProductById(tempId);
                        decimal totalprice = p.Price * count;
                        if (p != null)
                        {
                            if(totalprice < CurrentUser.Balance)
                            {
                                
                                for (int i = 0; i < count; i++) 
                                {
                                    stregSystem.BuyProduct(CurrentUser, p);
                                }
                                BuyTransaction t = new(p, CurrentUser);
                                stregsystemUI.DisplayUserBuysProduct(count, t);

                            }
                            else { stregsystemUI.DisplayInsufficientCash(CurrentUser, p); }
                               
                        }
                        else { stregsystemUI.DisplayProductNotFound(temp); }
                    }
                    catch { stregsystemUI.DisplayGeneralError(temp); }

                    
                }
            }
        }
        public void quit() 
        { 
            stregsystemUI.Close();
        }
        public void productChangeState(string command) 
        { 
            string newState = command.Split(' ')[0];
            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = stregSystem.GetProductById(Int32.Parse(proID));
                if (newState == ":activate")
                {
                    p.State = ProStat.Active;
                }
                else if (newState == ":deactivate") 
                {
                    p.State = ProStat.inActive;
                }
                stregSystem.AktiveProducts();
            }
            catch{ stregsystemUI.DisplayProductNotFound(proID); }
            restart();
        }
        public void ProductCeditEdit(string command) 
        {
            string cedit = command.Split(' ')[0];
            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = stregSystem.GetProductById(Int32.Parse(proID));
                if (cedit == ":crediton")
                {
                    p.CanBeBoughtOnCredit = true;
                }
                else if (cedit == ":creditoff")
                {
                    p.CanBeBoughtOnCredit = false;
                }
            }
            catch { stregsystemUI.DisplayProductNotFound(proID); }
            restart();
        }
        public void Addcredits(string command) 
        {
            string username = command.Split(' ')[1];
            string amount = command.Split(' ')[2];
            User u;
            try
            {
                u = stregSystem.GetUserByUsername(username);
                stregSystem.AddCreditsToAccount(u, Int32.Parse(amount));
            }
            catch { stregsystemUI.DisplayUserNotFound(username); }
            restart();
        }
    }
}
