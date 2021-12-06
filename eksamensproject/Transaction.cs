﻿namespace EksOP
{
    public class Transaction
    { 
        private static int prevId= 1;
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Transaction(User user, decimal amount) 
        { 
            Id = prevId++;
            prevId = prevId++;
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