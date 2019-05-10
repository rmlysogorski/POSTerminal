using System;
using System.Collections.Generic;

namespace POSTerminal
{
    class OrderController
    {
        private Order myOrder = new Order();
        private List<Product> productList;
        private PaymentInfo payType;

        public Order MyOrder
        {
            get { return myOrder; }
            set { myOrder = value; }
        }
        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; }
        }

        public PaymentInfo PayType
        {
            get { return payType; }
            set { payType = value; }
        }

        public OrderController(List<Product> _productList)
        {
            productList = _productList;
        }

        public void WelcomeAction()
        {
            IView menuView = new MenuView();
            menuView.Display();
            int input = int.Parse(Console.ReadLine()); //Needs Validation!!!!
            switch (input)
            {
                case 1:
                    CheckoutAction();
                    break;
                case 2: //SalesHistoryAction();
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }

        public void CheckoutAction()
        {
            IView cartView = new CartView(myOrder);
            cartView.Display();
            bool proceed = true;
            while (proceed)
            {
                int input = int.Parse(Console.ReadLine()); //Need Validation!!!
                switch (input)
                {
                    case 1:
                        AddProductToCart();
                        break;
                    case 2:
                        RemoveProductFromCart();
                        break;
                    case 3:
                        proceed = false;
                        ProcessPayment();
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        break;
                }
            }

        }

        public void AddProductToCart()
        {
            IView productListView = new ProductListView(productList);
            productListView.Display();
            int input = int.Parse(Console.ReadLine()); //Needs Validation!
            int quantity = int.Parse(Console.ReadLine()); //Needs Validation!
            for (int i = 0; i < quantity; i++)
            {
                myOrder.PurchaseList.Add(productList[input]);
                myOrder.Subtotal += productList[input].Price;
            }

            myOrder.Tax = myOrder.Subtotal * Tax.tax;

        }

        public void RemoveProductFromCart()
        {
            IView productListView = new ProductListView(myOrder.PurchaseList);
            productListView.Display();
            int input = int.Parse(Console.ReadLine()); //Needs Validation!
            int quantity = int.Parse(Console.ReadLine()); //Needs Validation!
            for (int i = 0; i < quantity; i++)
            {
                myOrder.PurchaseList.Remove(productList[input]);
                myOrder.Subtotal -= productList[input].Price;
            }
            myOrder.Tax = myOrder.Subtotal * Tax.tax;
        }

        public void ProcessPayment()
        {
            myOrder.Total = myOrder.Subtotal + myOrder.Tax;
            IView paymentView = new PaymentView(myOrder);
            paymentView.Display();
            int input = int.Parse(Console.ReadLine()); //Needs Validation!!!            
            switch (input)
            {
                case 1:
                    myOrder.PayInfo.PayType = "cash";
                    Console.WriteLine($"{myOrder.PayInfo.AmountTenderedMessage}");
                    myOrder.PayInfo.AmountTendered = double.Parse(Console.ReadLine());
                    Console.WriteLine($"{myOrder.PayInfo.ChangeMessage} {myOrder.PayInfo.Change}");
                    break;
                case 2:
                    myOrder.PayInfo.PayType = "credit";
                    break;
                case 3:
                    myOrder.PayInfo.PayType = "check";
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }

    }

}