using System;
using System.Collections.Generic;
using System.Linq;

namespace EksOP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            User u = new User("lars", "larsen", "larsen@g-mail.com", "ll_20");
            Console.WriteLine(u.ToString());  
        }
    }

}