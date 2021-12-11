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
        public void ParseCommand(string command) 
        {
            List<string> commands = command.Split().ToList<string>();
            if (commands.Contains(":"))
            {
                Console.WriteLine("here");    
            }
            else
            { 
                buyPhase(commands);
                Thread.Sleep(5000);
                Start();
            }
            
        }
        public void buyPhase(List<string> commands)
        {
            CurrentUser = stregSystem.GetUserByUsername(commands[0]);
            if (CurrentUser == null) { stregsystemUI.DisplayUserNotFound(commands[0]); }
            else
            {
                stregsystemUI.DisplayUserInfo(CurrentUser);
                commands.RemoveAt(0);
                if (commands.Count >= 1)
                {
                    foreach (string command in commands) 
                    { 
                        command.Trim('+');
                        try
                        {
                            stregsystemUI.DisplayUserBuysProduct(stregSystem.BuyProduct(CurrentUser, stregSystem.GetProductById(Convert.ToInt32(command))));

                        }
                        catch { stregsystemUI.DisplayProductNotFound(command); }
                    }
                }
                else 
                {
                    stregsystemUI.DisplayProduct();
                    Console.WriteLine();
                    Console.Write("Enter product id(s):");
                    string productids = Console.ReadLine();
                    List<string> ids = productids.Split('+').ToList<string>();
                    foreach (string id in ids)
                    {
                        try
                        {
                            stregsystemUI.DisplayUserBuysProduct(stregSystem.BuyProduct(CurrentUser, stregSystem.GetProductById(Convert.ToInt32(id))));

                        }
                        catch { stregsystemUI.DisplayProductNotFound(id); }
                    }
                }
            }
        }
    }
}
