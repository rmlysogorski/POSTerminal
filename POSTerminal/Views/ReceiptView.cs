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

        public ReceiptView(Order _myOrder)
        {
            myOrder = _myOrder;
        }

        public void Display()
        {
            Console.Clear();
            int headerSpacing = 20;
            if (myOrder.PayInfo.PayType == "cash")
            {
                Console.Write($"RSJ Coffee House\t\tOrder Number: {myOrder.OrderNumber}");
                Console.WriteLine($"\nDate: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}");
                Console.WriteLine();
                IView orderListView = new OrderListView(myOrder);
                orderListView.Display();
                Console.WriteLine();
                Console.WriteLine($"Subtotal{PrintDots("Subtotal", headerSpacing)}{myOrder.Subtotal:C2}");
                Console.WriteLine($"Tax{PrintDots("Tax", headerSpacing)}{myOrder.Tax:C2}");
                Console.WriteLine($"Total{PrintDots("Total", headerSpacing)}{myOrder.Total:C2}");
                Console.WriteLine($"Amount Tendered{PrintDots("Amount Tendered", headerSpacing)}{myOrder.PayInfo.AmountTendered:C2}");
                Console.WriteLine($"Change{PrintDots("Change", headerSpacing)}{myOrder.PayInfo.Change:C2}");
                Console.WriteLine($"\nThank you for shopping at RSJ Coffee House");
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

        public string PrintDots(string header, int length)
        {
            string dots = "";
            for(int i = 0; i < length - header.Length; i++)
            {
                dots += ".";
            }
            return dots;
        }
    }
}
