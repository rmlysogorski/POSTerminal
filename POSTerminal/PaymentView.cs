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
            throw new NotImplementedException();
        }
    }
}
