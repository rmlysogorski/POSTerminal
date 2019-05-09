using System;
using System.Collections.Generic;

namespace POSTerminal
{
    class Order
    {
        private List<Product> purchaseList;
        private double subtotal;
        private string payType;
        private double tax;
        private double total;
        private DateTime date;
        private int orderNumber;

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

        public string PayType
        {
            set { payType = value; }
            get { return payType; }
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

        public Order() { }

        public Order(List<string> _purchaseList, double _subtotal, string _payType, double _tax, double _total, DateTime _date, int _orderNumber)
        {
            purchaseList = _purchaseList;
            subtotal = _subtotal;
            payType = _payType;
            tax = _tax;
            total = _total;
            date = _date;
            orderNumber = _orderNumber;
        }


    }
}
