namespace EksOP
{
    public interface IStregsystemController
    {
        IStregSystem st { get; set; }
        IStregsystemUI ui { get; set; }

        void Addcredits(string command);
        void buyPhase(string command);
        void buyProducts(string amount, Product p);
        void ParseCommand(string command);
        void ProductCeditOff(string command);
        void ProductCeditOn(string command);
        void productSetActive(string command);
        void productSetinActive(string command);
        void quit();
        void restart();
        void Start();
       
    }
}