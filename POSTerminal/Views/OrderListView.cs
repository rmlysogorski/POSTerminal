using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class OrderListView : IView
    {
        private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public OrderListView(Order _myOrder)
        {
            myOrder = _myOrder;
        }

        public void ShowOrderDetail()
        {

            Console.WriteLine($"\n\nOrder#: {myOrder.OrderNumber} | Date: {myOrder.Date} | Total: {myOrder.Total:C2} | PayType: {myOrder.PayInfo.PayType}");
            foreach (Product p in myOrder.PurchaseList)
            {
                Console.Write($"{p.Name}({p.Qty}) @{p.Price:C2} ");
            }
        }

        public void Display()
        {
            string headers = string.Format("{0,-6}{1,-31}{2,-5}{3,-10}", "Item#", "Name", "Qty", "Price");
            string items;

            if (myOrder.PurchaseList.Count > 0)
            {
                Console.WriteLine(headers);

                for (int i = 0; i < myOrder.PurchaseList.Count; i++)
                {
                    items = string.Format("{0,-6}{1,-31}{2,-5}{3,-10:C2}",
                    i + 1,
                    myOrder.PurchaseList[i].Name,
                    myOrder.PurchaseList[i].Qty,
                    myOrder.PurchaseList[i].Qty * myOrder.PurchaseList[i].Price,
                    " "
                        );
                    Console.WriteLine(items);
                }
            }
            else
            {
                Console.WriteLine("No item in list");
            }
        }
    }
}
