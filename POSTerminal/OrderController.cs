using System;
using System.Collections.Generic;

namespace POSTerminal
{
    class OrderController
    {
        private Order myOrder;
        private List<Product> productList;

        public MyOrder
        {
            get {return myOrder; }
    set { myOrder = value; }
        }

        public ProductList
        {
            get {return productList; }
            set { productList = value; }
        }

        public OrderController(List<Product> _productList)
{
    productList = _productList;
}

public static WelcomeAction()
{
    myOrder = new Order();
    IView menuView = new MenuView();
    menuView.Display();
    string input = Console.ReadLine(); //Needs Validation!!!!
    switch (input)
    {
        case 1: CheckoutAction();
        case 2: SalesHistoryAction();
        default:
            Console.WriteLine("Invalid Selection");
            break;
    }
}

public static CheckoutAction()
{
    IView cartView = new CartView(myOrder);
    cartView.Display();
    bool proceed = true;
    while (proceed)
    {
        string input = Console.ReadLine();
        switch (input)
        {
            case 1: AddProductToCart();
            case 2: RemoveProductFromCart();
            case 3:
                proceed = false;
                ProccesPayment();
            default:
                Console.WriteLine("Invalid Selection");
                break;
        }
    }

}

public static AddProductToCart()
{
    IView productListView = new ProductListView(productList);
    productListView.Display();
    string input = Console.ReadLine(); //Needs Validation!
    int quantity = Console.ReadLine(); //Needs Validation!
    for (int i = 0; i < quantity; i++)
    {
        myOrder.purchaseList.Add(productList[input]);
    }

}

public static RemoveProductFromCart()
{
    IView productListView = new ProductListView(myOrder.purchaseList);
    productListView.Display();
    string input = Console.ReadLine();
    int quantity = Console.ReadLine();
    for (int i = 0; i < quantity; i++)
    {
        myOrder.purchaseList.Remove(productList[input]);
    }
}

public static ProcessPayment()
{
    IView paymentView = new PaymentView(myOrder);
    paymentView.Display();
    string input = Console.ReadLine(); //Needs Validation!!!
    switch (input)
    {
        case 1:
            IView cashView = new CashView(myOrder);
            cashView.Display();
        case 2:
            IView creditView = new CreditView(myOrder);
            creditView.Display();
        case 3:
            IView checkView = new CheckView(myOrder);
            checkView.Display();
        default:
            Console.WriteLine("Invalid Selection");
            break;
    }
}
    }
}
