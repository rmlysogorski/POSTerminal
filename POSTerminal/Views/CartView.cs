using System;

namespace POSTerminal
{
    class CartView : IView
    {
        private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public CartView(Order _myOrder)
        {
            myOrder = _myOrder;
        }        

        public void Display()
        {
            Console.Clear();
            IView orderListView = new OrderListView(myOrder);
            Console.WriteLine("Current Order: ");
            orderListView.Display();
            Console.WriteLine("\n1: Add an Item");
            Console.WriteLine("2: Remove an Item");
            Console.WriteLine("3: Cancel Order");
            Console.WriteLine("4: Proceed to Checkout");
            Console.Write("\nMake a slection(1-4): ");

        }
    }
}
