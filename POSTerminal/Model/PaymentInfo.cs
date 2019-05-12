using System;

namespace POSTerminal
{
    enum PayType
    {
        Cash = 1,
        Credit,
        Check
    }

    class PaymentInfo
    {
        private PayType payType;
        private double amountTendered;
        private double change;
        private string cardNumber;
        private int cvv;
        private DateTime expirationDate;
        private double cashBack;
        private string checkNumber;

        private string checkNumberMessage = "Enter the check number: ";
        private string creditCardNumberMessage = "Enter the credit card number: ";
        private string cvvMessage = "Enter the CVV: ";
        private string expirationDateMessage = "Enter the expiration date: ";
        private string cashBackMessage = "Their cash back totals: ";
        private string amountTenderedMessage = "Enter the amount tendered: ";
        private string changeMessage = "Their change is: ";


        #region payType
        public PayType PayType
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

        public PaymentInfo()
        {
            payType = PayType.Cash;
            amountTendered = 0.00;
            change = 0.00;
            cardNumber = "";
            cvv = 0;
            cashBack = 0;
            checkNumber = "";
            expirationDate = DateTime.Parse("01/01/1000"); ;
        }


    }
}
