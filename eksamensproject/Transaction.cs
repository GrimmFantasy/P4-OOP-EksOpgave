namespace EksOP
{
    public class Transaction
    { 
        private static uint _prevId= 0;
        public uint Id { get; }
        public User User { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }
        public Transaction(User user, decimal amount) 
        { 
            Id = ++_prevId;
            User = user;
            Amount = amount;
            Date = DateTime.Now;
        }
        public override string ToString() 
        { 
            return Id.ToString() + " ; " + User.ToString() + " ; " + Amount.ToString() + " ; " + Date.ToString();
        }
    }
}