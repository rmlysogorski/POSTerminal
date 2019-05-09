namespace POSTerminal
{
    abstract class Payment
    {
        private string payType;
        private string amountTenderedMessage = "Enter the amount tendered: ";
        private string changeMessage = "Their change is: ";
        private string creditCardNumberMessage = "Enter the credit card number: ";
        private string cvvMessage = "Enter the CVV: ";
        private string expirationDateMessage = "Enter the expiration date: ";
        private string cashBackMessage = "Their cash back totals: ";
        private string checkNumberMessage = "Enter the check number: ";

        public string PayType
        {
            get { return payType; }
            set { payType = value; }
        }

        public string AmountTenderedMessage
        {
            get { return amountTenderedMessage; }
        }

        public string ChangeMessage
        {
            get { return changeMessage; }
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

        public string CheckNumberMessage
        {
            get { return checkNumberMessage; }
        }

        public Payment() { }

        public Payment(string _payType)
        {
            payType = _payType;
        }

    }
}
