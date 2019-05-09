using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class CheckView : IView
    {

        private string checkNumber; private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public string CheckNumber
        {
            get { return checkNumber; }
            set { checkNumber = value; }
        }

        public CheckView(Order _myOrder)
        {
            myOrder = _myOrder;
        }

        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
