using System;

namespace POSTerminal
{
    class Credit : Payment
    {
        private string cardNumber;
        private int cvv;
        private DateTime expirationDate;
        private double cashBack;
    }
}
