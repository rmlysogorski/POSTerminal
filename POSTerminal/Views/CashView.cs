using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class CashView : IView
    {
        private double amountTendered;
        private double change; private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public double AmountTendered
        {
            get { return amountTendered; }
            set { amountTendered = value; }
        }

        public double Change
        {
            get { return change; }
            set { change = value; }
        }

        public CashView(Order _myOrder)
        {
            myOrder = _myOrder;
        }

        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
