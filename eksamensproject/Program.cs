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
            IStregSystem stregSystem = new StregSystem();
            IStregsystemUI st = new StregsystemUI(stregSystem);
            IStregsystemController cl = new StregsystemController(stregSystem, st);
            cl.Start();
            //st.DisplayProduct();
        }
    }

}