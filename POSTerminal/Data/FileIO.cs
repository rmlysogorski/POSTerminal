using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;

namespace POSTerminal
{
    class FileIO
    {

        public static List<Product> GetFromProductFile()
        {
            List<Product> productList = new List<Product>();

            string fileName = @"..\\..\\Data\\productList.txt";
            string binaryPath = AppDomain.CurrentDomain.BaseDirectory + fileName;                       

            StreamReader myFile = new StreamReader(binaryPath);

            try
            {
                string line;

                while ((line = myFile.ReadLine()) != null)
                {
                    Product productToAdd = new Product(line.Split('|')[0], line.Split('|')[1], double.Parse(line.Split('|')[2]), line.Split('|')[3]);
                    productList.Add(productToAdd);
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("There was an error reading the product list files...");
            }
            finally
            {
                myFile.Close();
            }

            return productList;
        }

        public static void WriteToProductFile(Product product)
        {

            string fileName = @"..\\..\\Data\\productList.txt";
            string binaryPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
            string delimiter = "|";

            StreamWriter myFileWriter = new StreamWriter(binaryPath, true);

            try
            {
                myFileWriter.Write("\n" + product.Name + delimiter + product.Category + delimiter + product.Price + delimiter + product.Description);
            }
            catch (System.Exception)
            {
                throw new System.Exception("There was an error writing to product list files...");
            }
            finally
            {
                myFileWriter.Close();
            }


        }
    }
}
