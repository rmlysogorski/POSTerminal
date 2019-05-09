using System.Collections.Generic;
using System.IO;

namespace POSTerminal.Data
{
    class FileIO
    {

        public static List<Product> GetDataFile()
        {
            List<Product> productList = new List<Product>();

            string fileName = @"\\Data\\productList.txt";
            string binaryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + fileName;

            StreamReader myFile = new StreamReader(binaryPath);
            
            try
            {
                string line; //stores the file data for one line

                while ((line = myFile.ReadLine()) != null) //while line exists
                {
                    Product productToAdd = new Product(line.Split('|')[0], line.Split('|')[1], double.Parse(line.Split('|')[2]), line.Split('|')[3]); //Create a dummy Country 
                    productList.Add(productToAdd); //Put the Country in the list 
                }
            }
            catch(System.Exception)
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

            string fileName = @"\\Data\\productList.txt";
            string binaryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + fileName;
            string delimiter = "|";

            StreamWriter myFileWriter = new StreamWriter(binaryPath);

            try
            {
                myFileWriter.WriteLine(product.Name + delimiter + product.Category + delimiter + product.Price + delimiter + product.Description);  
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
