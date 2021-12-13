namespace EksOP
{
    public class StregsystemController
    {
        public StregSystem st { get; set; }
        public IStregsystemUI ui { get; set;}
        private User CurrentUser { get; set; }
        public StregsystemController(StregSystem streg, IStregsystemUI uI)
        {
            this.st = streg;
            this.ui = uI;
        }
        public void Start() 
        {
            ui.Start();
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
                        ui.DisplayAdminCommandNotFoundMessage(command);
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
            CurrentUser = st.GetUserByUsername(commands[0]);
            if (CurrentUser == null) { ui.DisplayUserNotFound(commands[0]); }
            else
            {
                ui.DisplayUserInfo(CurrentUser);
                commands.RemoveAt(0);
                Product p;
                if (commands.Count < 1)
                {
                    ui.DisplayProduct();
                    Console.WriteLine();
                    Console.Write("Enter product id(s):");
                    string productids = Console.ReadLine();
                    List<string> ids = productids.Split('+').ToList<string>();
                    
                    foreach (string id in ids)
                    {

                        p = st.GetActiveProductById(Convert.ToInt32(id));
                        if (p != null) 
                        { 
                            try
                            {
                                ui.DisplayUserBuysProduct(st.BuyProduct(CurrentUser, p));
                            }
                            catch { ui.DisplayInsufficientCash(CurrentUser, p); }
                        }
                        else { ui.DisplayProductNotFound(id); }    
                    }
                    
                }
                else 
                {
                    string temp = commands.Last();
                    try
                    {
                        int tempId = Int32.Parse(commands.Last());
                        int count = Int32.Parse(commands.First());
                            
                        p = st.GetActiveProductById(tempId);
                        decimal totalprice = p.Price * count;
                        if (p != null)
                        {
                            if(totalprice < CurrentUser.Balance)
                            {
                                
                                for (int i = 0; i < count; i++) 
                                {
                                    st.BuyProduct(CurrentUser, p);
                                }
                                BuyTransaction t = new(p, CurrentUser);
                                ui.DisplayUserBuysProduct(count, t);

                            }
                            else { ui.DisplayInsufficientCash(CurrentUser, p); }
                               
                        }
                        else { ui.DisplayProductNotFound(temp); }
                    }
                    catch { ui.DisplayNotProductID(temp); }
                }
            }
        }
        public void quit() 
        { 
            ui.Close();
        }
        public void productChangeState(string command) 
        { 
            string newState = command.Split(' ')[0];
            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = st.GetProductById(Int32.Parse(proID));
                if (newState == ":activate")
                {
                    p.State = ProStat.Active;
                }
                else if (newState == ":deactivate") 
                {
                    p.State = ProStat.inActive;
                }
                st.AktiveProducts();
            }
            catch{ ui.DisplayProductNotFound(proID); }
            restart();
        }
        public void ProductCeditEdit(string command) 
        {
            string cedit = command.Split(' ')[0];
            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = st.GetProductById(Int32.Parse(proID));
                if (cedit == ":crediton")
                {
                    p.CanBeBoughtOnCredit = true;
                }
                else if (cedit == ":creditoff")
                {
                    p.CanBeBoughtOnCredit = false;
                }
            }
            catch { ui.DisplayProductNotFound(proID); }
            restart();
        }
        public void Addcredits(string command) 
        {
            string username = command.Split(' ')[1];
            string amount = command.Split(' ')[2];
            User u;
            try
            {
                u = st.GetUserByUsername(username);
                st.AddCreditsToAccount(u, Int32.Parse(amount));
            }
            catch { ui.DisplayUserNotFound(username); }
            restart();
        }
    }
}
