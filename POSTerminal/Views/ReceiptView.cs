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
            if (myOrder.PayInfo.PayType == PayType.Cash)
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
            else if (myOrder.PayInfo.PayType == PayType.Credit)
            {
                Console.Write($"RSJ Coffee House\t\tOrder Number: {myOrder.OrderNumber}");
                Console.WriteLine($"\nDate: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}");
                IView orderListView = new OrderListView(myOrder);
                orderListView.Display();
                Console.WriteLine($"Subtotal{PrintDots("Subtotal", headerSpacing)}{myOrder.Subtotal:C2}");
                Console.WriteLine($"Tax{PrintDots("Tax", headerSpacing)}{myOrder.Tax:C2}");
                Console.WriteLine($"Total{PrintDots("Total", headerSpacing)}{myOrder.Total:C2}");
                Console.WriteLine($"Card Number{PrintDots("Card Number", headerSpacing)}**** **** **** {myOrder.PayInfo.CardNumber.Substring(myOrder.PayInfo.CardNumber.Length -4)}");
                Console.WriteLine($"Amount Tendered{PrintDots("Amount Tendered", headerSpacing)}{myOrder.PayInfo.AmountTendered:C2}");
                Console.WriteLine($"Cash Back{PrintDots("Cash Back", headerSpacing)}{myOrder.PayInfo.CashBack}");
                Console.WriteLine($"\nThank you for shopping at RSJ Coffee House");
            }
            else
            {
                Console.Write($"RSJ Coffee House\t\tOrder Number: {myOrder.OrderNumber}");
                Console.WriteLine($"\nDate: {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}");
                IView orderListView = new OrderListView(myOrder);
                orderListView.Display();
                Console.WriteLine($"Subtotal{PrintDots("Subtotal", headerSpacing)}{myOrder.Subtotal:C2}");
                Console.WriteLine($"Tax{PrintDots("Tax", headerSpacing)}{myOrder.Tax:C2}");
                Console.WriteLine($"Total{PrintDots("Total", headerSpacing)}{myOrder.Total:C2}");
                Console.WriteLine($"Check Number{PrintDots("Check Number", headerSpacing)}{myOrder.PayInfo.CheckNumber}");
                Console.WriteLine($"Amount Tendered{PrintDots("Amount Tendered", headerSpacing)}{myOrder.PayInfo.AmountTendered:C2}");
                Console.WriteLine($"\nThank you for shopping at RSJ Coffee House");
            }

            Console.ReadKey();
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
