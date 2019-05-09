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
            IView orderListView = new OrderListView(myOrder);
            Console.WriteLine("Current Order: ");
            orderListView.Display();
            Console.WriteLine("1: Add an Item");
            Console.WriteLine("2: Remove an Item");
            Console.WriteLine("3: Cancel Order");
            Console.WriteLine("4: Proceed to Checkout");

        }
    }
}
