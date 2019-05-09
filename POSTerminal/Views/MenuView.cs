using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class MenuView : IView
    {
        public void Display()
        {

            Console.WriteLine("Hello!  Welcome to our store!");
            Console.WriteLine("1: Checkout");
            Console.WriteLine("2: Sales History");
            Console.WriteLine("3: Quit");
          

        }
    }
}
