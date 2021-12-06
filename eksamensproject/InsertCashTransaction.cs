namespace EksOP
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base(user ,amount)
        {

        }
        public override string ToString() 
        {
            return Id.ToString()+";" + User.UserName.ToString() + ";" + Amount + ";" + Date.ToString();
        }
        public void Execute() 
        {
            User.Balance = User.Balance + Amount;
        }

    }
}