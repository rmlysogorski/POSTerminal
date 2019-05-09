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
        private DateTime expirationDate;

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

        public CreditCardView()
        {

        }

        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
