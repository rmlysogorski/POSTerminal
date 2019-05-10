using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class ReceiptView : IView
    {
        private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public void Display()
        {
            if (myOrder.PayInfo.PayType == "cash")
            {
                Console.Write($"RSJ Coffee House\t\tOrder Number: {myOrder.OrderNumber}");
                Console.WriteLine($"\t\tDate: {DateTime.Now.ToString("yyyyMMddHHmmss")}");
                Console.WriteLine($"{myOrder.PurchaseList}");
                Console.WriteLine($"\t\tSubtotal: {myOrder.Subtotal}");
                Console.WriteLine($"\t\tTax: {myOrder.Tax}");
                Console.WriteLine($"\t\tTotal:{myOrder.Total}");
                Console.WriteLine($"\t\tAmount Tendered: {myOrder.PayInfo.AmountTendered}");
                Console.WriteLine($"\t\tChange: {myOrder.PayInfo.Change}");
                Console.WriteLine($"Thank you for shopping at RSJ Coffee House");
            }
            else if (myOrder.PayInfo.PayType == "credit card")
            {
                Console.Write($"RSJ Coffee House\t\tOrder Number: {myOrder.OrderNumber}");
                Console.WriteLine($"\t\tDate: {DateTime.Now.ToString("yyyyMMddHHmmss")}");
                Console.WriteLine($"{myOrder.PurchaseList}");
                Console.WriteLine($"\t\tSubtotal: {myOrder.Subtotal}");
                Console.WriteLine($"\t\tTax: {myOrder.Tax}");
                Console.WriteLine($"\t\tTotal:{myOrder.Total}");
                Console.Write($"\tCard Number: {myOrder.PayInfo.CardNumber}");
                Console.WriteLine($"\t\tAmount Tendered: {myOrder.PayInfo.AmountTendered}");
                Console.WriteLine($"\t\tCash Back: {myOrder.PayInfo.CashBack}");
                Console.WriteLine($"Thank you for shopping at RSJ Coffee House");
            }
            else
            {
                Console.Write($"RSJ Coffee House\t\tOrder Number: {myOrder.OrderNumber}");
                Console.WriteLine($"\t\tDate: {DateTime.Now.ToString("yyyyMMddHHmmss")}");
                Console.WriteLine($"\t{myOrder.PurchaseList}");
                Console.WriteLine($"\t\tSubtotal: {myOrder.Subtotal}");
                Console.WriteLine($"\t\tTax: {myOrder.Tax}");
                Console.WriteLine($"\t\tTotal: {myOrder.Total}");
                Console.Write($"\tCheck Number: {myOrder.PayInfo.CheckNumber}");
                Console.WriteLine($"\t\tAmount Tendered: {myOrder.PayInfo.AmountTendered}");
                Console.WriteLine($"Thank you for shopping at RSJ Coffee House");
            }
        }
    }
}
