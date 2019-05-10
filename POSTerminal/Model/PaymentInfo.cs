using System;

namespace POSTerminal
{
    class PaymentInfo
    {
        private string payType;

        private double amountTendered;
        private double change;
        private string amountTenderedMessage = "Enter the amount tendered: ";
        private string changeMessage = "Their change is: ";

        private string cardNumber;
        private int cvv;
        private DateTime expirationDate;
        private double cashBack;
        private string creditCardNumberMessage = "Enter the credit card number: ";
        private string cvvMessage = "Enter the CVV: ";
        private string expirationDateMessage = "Enter the expiration date: ";
        private string cashBackMessage = "Their cash back totals: ";

        private string checkNumber;
        private string checkNumberMessage = "Enter the check number: ";

        #region payType
        public string PayType
        {
            get { return payType; }
            set { payType = value; }
        }
        #endregion

        #region Cash
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

        public string AmountTenderedMessage
        {
            get { return amountTenderedMessage; }
        }

        public string ChangeMessage
        {
            get { return changeMessage; }
        }

        #endregion

        #region Credit
        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }

        public int Cvv
        {
            get { return cvv; }
            set { cvv = value; }
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        public double CashBack
        {
            get { return cashBack; }
            set { cashBack = value; }
        }

        public string CreditCardNumberMessage
        {
            get { return creditCardNumberMessage; }
        }

        public string CvvMessage
        {
            get { return cvvMessage; }
        }

        public string ExpirationDateMessage
        {
            get { return expirationDateMessage; }
        }

        public string CashBackMessage
        {
            get { return cashBackMessage; }
        }
        #endregion

        #region Check
        public string CheckNumber
        {
            get { return checkNumber; }
            set { checkNumber = value; }
        }

        public string CheckNumberMessage
        {
            get { return checkNumberMessage; }
        }
        #endregion

        public PaymentInfo() { }

        public PaymentInfo(string _payType)
        {
            payType = _payType;
        }

    }
}
