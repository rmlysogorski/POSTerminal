
using System;
using System.Collections.Generic;

namespace POSTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> productList = FileIO.GetFromProductFile();
            OrderController orderController = new OrderController(productList);

            orderController.WelcomeAction();

            //foreach (Product p in productList)
            //{
            //    System.Console.WriteLine(p.Name);
            //}


        }
    }
}
