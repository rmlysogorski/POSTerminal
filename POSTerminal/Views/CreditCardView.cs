using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class CreditCardView : IView
    {
        private string  cardNumber;
        private int cvv;
        private DateTime expirationDate; private Order myOrder;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }

        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }

        public int CVV
        {
            get { return cvv; }
            set { cvv = value; }
        }

        public DateTime Expiration
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        public CreditCardView(Order _myOrder)
        {
            myOrder = _myOrder;
        }

        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
