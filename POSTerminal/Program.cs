﻿using POSTerminal.Data;
using System.Collections.Generic;

namespace POSTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> productList = FileIO.GetDataFile();

            foreach(Product p in productList)
            {
                System.Console.WriteLine(p.Name);
            }
        }
    }
}
