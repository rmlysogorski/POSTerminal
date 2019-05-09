using System;

namespace POSTerminal
{
    class PaymentView : IView
    {
        private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public PaymentView(Order _myOrder)
        {
            myOrder = _myOrder;
        }

        public void Display()
        {
            Console.WriteLine($"Subtotal: {myOrder.Subtotal:C2} ");
            Console.WriteLine($"Tax: {myOrder.Tax:C2} ");
            Console.WriteLine($"Total: {myOrder.Total:C2} ");
            Console.WriteLine($"Enter payment method: 1: Cash, 2: Credit, 3: Check");
        }
    }
}
