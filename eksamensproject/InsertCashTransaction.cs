namespace EksOP
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base(user ,amount)
        {

        }
        public override string ToString() 
        {
            return "Deposit:" + User.UserName.ToString() + " deposited " + Amount + " on the " + Date.ToString() + ". Id: " + Id.ToString();
        }
        public void Execute() 
        {
            User.Balance = User.Balance + Amount;
        }

    }
}