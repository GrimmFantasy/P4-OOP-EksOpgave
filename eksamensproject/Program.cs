using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace EksOP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StregSystem stregSystem = new StregSystem();
            Console.WriteLine("users");
            stregSystem.ReadUsers();
            foreach (User u in stregSystem.Users) 
            { 
            Console.WriteLine(u.ToString() + " " + u.Id);  
                
            } 
            Console.WriteLine("Product");
            stregSystem.ReadProduct();
            stregSystem.AktiveProducts();
            foreach (Product p in stregSystem.ActiveProducts) 
            { 
            Console.WriteLine(p.ToString() + " id: " + p.State);  
                
            }
            User u1 = new User("lars", "lars", "dn2dn.com", "dn");
            InsertCashTransaction i = new InsertCashTransaction(u1, 100);
            i.Execute();
            stregSystem.Log.Add(i);
            Product e = new Product(456789, "nuts", 100, 1);
            BuyTransaction b = new BuyTransaction(e, u1);
            b.Execute();
            stregSystem.Log.Add(b);
            stregSystem.WriteLog();
        }
    }

}