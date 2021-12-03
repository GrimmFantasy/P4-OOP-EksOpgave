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

        }
    }

}