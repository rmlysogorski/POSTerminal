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
            throw new NotImplementedException();
        }
    }
}
