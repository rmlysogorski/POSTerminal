using System;
using System.Collections.Generic;

namespace POSTerminal
{    

    class Order
    {
        private int orderNumber;
        private double subtotal;
        private double tax;
        private double total;
        private DateTime date;
        private List<Product> purchaseList;
        private PaymentInfo payInfo;

        public List<Product> PurchaseList
        {
            set { purchaseList = value; }
            get { return purchaseList; }
        }
        public double Subtotal
        {
            set { subtotal = value; }
            get { return subtotal; }
        }

        public PaymentInfo PayInfo
        {
            set { payInfo = value; }
            get { return payInfo; }
        }

        public double Tax
        {
            set { tax = value; }
            get { return tax; }
        }

        public double Total
        {
            set { total = value; }
            get { return total; }
        }

        public DateTime Date
        {
            set { date = value; }
            get { return date; }
        }
        public int OrderNumber
        {
            set { orderNumber = value; }
            get { return orderNumber; }
        }

        public Order()
        {
            purchaseList = new List<Product>();
            payInfo = new PaymentInfo();
        }

        public Order(List<Product> _purchaseList, double _subtotal, PaymentInfo _payType, double _tax, double _total, DateTime _date, int _orderNumber)
        {
            purchaseList = _purchaseList;
            subtotal = _subtotal;
            payInfo = _payType;
            tax = _tax;
            total = _total;
            date = _date;
            orderNumber = _orderNumber;
        }


    }
}
