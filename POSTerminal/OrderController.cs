using System;
using System.Collections.Generic;

namespace POSTerminal
{
    class OrderController
    {
        private Order myOrder = new Order();
        private List<Product> productList;

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
            }

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
            }
        }

        public void ProcessPayment()
        {
            IView paymentView = new PaymentView(myOrder);
            paymentView.Display();
            int input = int.Parse(Console.ReadLine()); //Needs Validation!!!
            switch (input)
            {
                case 1:
                    IView cashView = new CashView(myOrder);
                    cashView.Display();
                    break;
                case 2:
                    IView creditView = new CreditCardView(myOrder);
                    creditView.Display();
                    break;
                case 3:
                    IView checkView = new CheckView(myOrder);
                    checkView.Display();
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }
        }

    }

}