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
            foreach (Product p in stregSystem.Products) 
            { 
            Console.WriteLine(p.ToString() + " " + p.Id);  
                
            }
        }
    }

}