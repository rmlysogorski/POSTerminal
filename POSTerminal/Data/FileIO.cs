using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static List<Order> GetFromOrderFileByDate(DateTime date)
        {
            List<Order> OrderList = new List<Order>();

            string fileNameWithPath = @"..\\..\\Data\\OrderList.txt";

            StreamReader myFile = new StreamReader(fileNameWithPath);

            try
            {
                string line;

                while ((line = myFile.ReadLine()) != null)
                {
                    if (DateTime.Parse(line.Split('|')[4]).Date == DateTime.Today.Date)
                    {
                        OrderList.Add(GetOrderFromLine(line.Split(';')));
                    }
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

            return OrderList;
        }

        private static Order GetOrderFromLine(string[] orderComponents)
        {
            Order order = new Order();

            order.PayInfo.PayType = (PayType)Enum.Parse(typeof(PayType), orderComponents[2].Split('|')[0]);

            string[] products = orderComponents[1].Split('?');
            int index = 0;
            try
            {
                foreach (string p in products)
                {
                    if (p != string.Empty)
                    {
                        order.PurchaseList.Add(new Product());
                        order.PurchaseList[index].Name = p.Split('|')[0];
                        order.PurchaseList[index].Qty = int.Parse(p.Split('|')[1]);
                        order.PurchaseList[index].Price = double.Parse(p.Split('|')[2]);
                    }
                    index++;
                }

                order.PayInfo.AmountTendered = double.Parse(orderComponents[2].Split('|')[1]);
                order.PayInfo.Change = double.Parse(orderComponents[2].Split('|')[2]);
                order.PayInfo.CardNumber = orderComponents[2].Split('|')[3];
                order.PayInfo.Cvv = int.Parse(orderComponents[2].Split('|')[4]);
                order.PayInfo.ExpirationDate = DateTime.Parse(orderComponents[2].Split('|')[5]);
                order.PayInfo.CashBack = double.Parse(orderComponents[2].Split('|')[6]);
                order.PayInfo.CheckNumber = orderComponents[2].Split('|')[7];

                order.OrderNumber = int.Parse(orderComponents[0].Split('|')[0]);
                order.Subtotal = double.Parse(orderComponents[0].Split('|')[1]);
                order.Tax = double.Parse(orderComponents[0].Split('|')[2]);
                order.Total = double.Parse(orderComponents[0].Split('|')[3]); ;
                order.Date = DateTime.Parse(orderComponents[0].Split('|')[4]);

                return order;
            }
            catch (Exception)
            {
                throw new Exception("Was not able to read orderList.txt");
            }
        }


        public static int GetLastOrderNumber()
        {
            StreamReader reader = new StreamReader(@"..\\..\\Data\\OrderList.txt");
            string lastLine = "";
            string line;

            try
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lastLine = line;
                }
                reader.Close();


                return int.Parse(lastLine.Split('|')[0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public static void WriteToOrderFile(Order order)
        {

            string delimiter = "|";
            string productDelimiter = ";";
            string innerGroup = "?";


            try
            {
                string productsToWrite = productDelimiter;

                if (order.PurchaseList.Count > 0)
                {
                    foreach (Product p in order.PurchaseList)
                    {
                        productsToWrite += (p.Name + delimiter + p.Qty + delimiter + p.Price + innerGroup);
                    }
                }

                productsToWrite += productDelimiter;

                string payInfoToWrite = "";

                payInfoToWrite = order.PayInfo.PayType + delimiter +
                    order.PayInfo.AmountTendered + delimiter + order.PayInfo.Change.ToString("0.00") + delimiter +
                    order.PayInfo.CardNumber + delimiter + order.PayInfo.Cvv + delimiter +
                    order.PayInfo.ExpirationDate.ToShortDateString() + delimiter + order.PayInfo.CashBack + delimiter +
                    order.PayInfo.CheckNumber + productDelimiter;

                using (StreamWriter outputFile = new StreamWriter(@"..\\..\\Data\\OrderList.txt", true))
                {
                    outputFile.WriteLine(order.OrderNumber + delimiter + order.Subtotal +
                     delimiter + order.Tax + delimiter + order.Total + delimiter + DateTime.Now + delimiter +
                     productsToWrite + payInfoToWrite);
                    outputFile.Close();
                }

            }
            catch (System.Exception)
            {
                throw new System.Exception("There was an error writing to product list files...");
            }
        }

        public static void RemoveLineFromFile(int index)
        {
            string fileName = @"..\\..\\Data\\productList.txt";
            string binaryPath = AppDomain.CurrentDomain.BaseDirectory + fileName;

            try
            {
                List<string> linesList = File.ReadAllLines(binaryPath).ToList();
                linesList.RemoveAt(index);
                using (StreamWriter sw = new StreamWriter(binaryPath))
                {
                    int count = 0;
                    foreach (string line in linesList)
                    {
                        if (count < linesList.Count - 1)
                        {
                            sw.WriteLine(line);
                        }
                        else
                        {
                            sw.Write(line);
                        }
                        count++;
                    }

                }

            }
            catch (Exception e)
            {
                IMessage view = new MessageView();
                view.Display(e.Message);
            }

        }
    }
}
