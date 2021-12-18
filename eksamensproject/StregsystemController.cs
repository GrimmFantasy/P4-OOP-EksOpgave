namespace EksOP
{
    public class StregsystemController : IStregsystemController
    {
        private User CurrentUser { get; set; }
        public IStregSystem st { get; set; }
        public IStregsystemUI ui { get; set; }
        public StregsystemController(IStregSystem streg, IStregsystemUI uI)
        {
            this.st = streg;
            this.ui = uI;
            ui.Command += ParseCommand;
        }
        public void Start()
        {
            ui.Start();
            string command = Console.ReadLine();
            ParseCommand(command);
        }
        public void restart()
        {
            Thread.Sleep(3000);
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
                        productSetActive(command);
                        break;
                    case ":deactivate":
                        productSetinActive(command);
                        break;
                    case ":crediton":
                        ProductCeditOn(command);
                        break;
                    case ":creditoff":
                        ProductCeditOff(command);
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
            Product p;
            if (CurrentUser == null) { ui.DisplayUserNotFound(commands[0]); }
            else
            {
                ui.DisplayUserInfo(CurrentUser);
                commands.RemoveAt(0);
                //if (commands.Count < 1 || commands.Last() == "" && commands.Count <= 1)
                //{
                //    Console.WriteLine();
                //    Console.Write("Enter product id(s):");
                //    string productids = Console.ReadLine();
                //    List<string> ids = productids.Split('+').ToList<string>();

                //    foreach (string id in ids)
                //    {
                //        try
                //        {
                //            if (id.Split(' ')[0] == " " || id.Split(' ')[0] == "" || id.Split(' ')[1] == " " || id.Split(' ')[1] == "")
                //            {
                //                p = st.GetActiveProductById(Convert.ToInt32(id));
                //                buyProducts("1", p);
                //            }
                //            else
                //            {

                //                p = st.GetActiveProductById(Convert.ToInt32(id.Split(' ')[1]));
                //                buyProducts(id.Split(' ')[0], p);
                //            }
                //        }
                //        catch { ui.DisplayProductNotFound(id); }
                //    }
                //}
                //else
                //{
                if (commands.Count >= 1) 
                { 
                    try
                    {
                        int Id;
                        string amount;
                        if (commands.Count ==  1)
                        {
                            Id = Int32.Parse(commands.First());
                            amount = "1";
                        }
                        else
                        {
                            Id = Int32.Parse(commands.Last());
                            amount = commands.First();
                        }
                        try
                        {
                            p = st.GetActiveProductById(Id);

                            buyProducts(amount, p);
                        }
                        catch { ui.DisplayProductNotFound(Id.ToString()); }

                    }
                    catch { ui.DisplayNotProductID(commands.Last()); }
                }
                //}
            }
        }
        public void buyProducts(string amount, Product p)
        {

            try
            {
                int count = 1;
                if (amount != "1") { count = Convert.ToInt32(amount); }
                decimal totalprice = p.Price * count;
                if (CurrentUser.Balance - totalprice >= 0 || p.CanBeBoughtOnCredit)
                {
                    BuyTransaction t = st.BuyProduct(CurrentUser, p);
                    if (count > 1)
                    {
                        for (int i = 0; i < count - 1; i++)
                        {
                            t = st.BuyProduct(CurrentUser, p);
                        }
                        ui.DisplayUserBuysProduct(count, t);
                    }
                    else
                    {
                        ui.DisplayUserBuysProduct(t);
                    }

                }
                else { ui.DisplayInsufficientCash(CurrentUser, p); }
            }
            catch { Console.WriteLine(amount); }
        }
        public void quit()
        {
            ui.Close();
        }

        public void productSetinActive(string command)
        {
            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = st.GetProductById(Int32.Parse(proID));
                p.State = ProStat.inActive;
            }
            catch { ui.DisplayProductNotFound(proID); }
            restart();
        }

        public void productSetActive(string command)
        {

            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = st.GetProductById(Int32.Parse(proID));
                p.State = ProStat.Active;
            }
            catch { ui.DisplayProductNotFound(proID); }
            restart();
        }
        public void ProductCeditOff(string command)
        {

            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = st.GetProductById(Int32.Parse(proID));
                p.CanBeBoughtOnCredit = false;

            }
            catch { ui.DisplayProductNotFound(proID); }
            restart();
        }
        public void ProductCeditOn(string command)
        {
            string proID = command.Split(' ')[1];
            Product p;
            try
            {
                p = st.GetProductById(Int32.Parse(proID));
                p.CanBeBoughtOnCredit = true;
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
