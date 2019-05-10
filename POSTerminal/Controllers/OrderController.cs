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
            int input = UserInput.GetUserInputAsInteger("");
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

            bool proceed = true;
            while (proceed)
            {
                cartView.Display();
                int input = UserInput.GetUserInputAsInteger("");
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
                        break;
                    case 4:
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
            ProductListView productListView = new ProductListView(productList);
            IMessage view = new MessageView();
            int quantity = 0;
            view.Display("1: Search By Name\n2: Search By Category\n3: List All Products");
            int input = UserInput.GetUserInputAsInteger("");

            switch (input)
            {
                case 1:

                    view.Display("Enter product name: ");
                    string name = UserInput.GetUserInput("");
                    ProductListView filteredList = new ProductListView(productListView.GetFilteredList("name", name));
                    filteredList.Display();
                    view.Display(string.Format("\nChoose a product (1-{0})", filteredList.ProductList.Count));
                    input = UserInput.GetUserInputAsInteger("");
                    input--;
                    view.Display("Enter Quantity: ");
                    quantity = UserInput.GetUserInputAsIntegerOrReturnOne("");


                    if (myOrder.PurchaseList.Contains(filteredList.ProductList[input]))
                    {
                        myOrder.PurchaseList.Find(x => x.Name == filteredList.ProductList[input].Name).Qty += quantity;
                    }
                    else
                    {

                        myOrder.PurchaseList.Add(filteredList.ProductList[input]);
                        myOrder.PurchaseList.Find(x => x.Name == filteredList.ProductList[input].Name).Qty += quantity;

                    }

                    break;
                case 2:
                    view.Display("Enter product category: ");
                    string category = UserInput.GetUserInput("");
                    ProductListView filteredListByCat = new ProductListView(productListView.GetFilteredList("category", category));
                    filteredListByCat.Display();
                    view.Display(string.Format("\nChoose a product (1-{0})", filteredListByCat.ProductList.Count));
                    input = UserInput.GetUserInputAsInteger("");
                    input--;
                    view.Display("Enter Quantity: ");
                    quantity = UserInput.GetUserInputAsIntegerOrReturnOne("");


                    if (myOrder.PurchaseList.Contains(filteredListByCat.ProductList[input]))
                    {
                        myOrder.PurchaseList.Find(x => x.Name == filteredListByCat.ProductList[input].Name).Qty += quantity;
                    }
                    else
                    {

                        myOrder.PurchaseList.Add(filteredListByCat.ProductList[input]);
                        myOrder.PurchaseList.Find(x => x.Name == filteredListByCat.ProductList[input].Name).Qty += quantity;

                    }

                    break;
                case 3:
                    productListView.Display();
                    view.Display(string.Format("\nChoose a product (1-{0})", productList.Count));
                    input = UserInput.GetUserInputAsInteger("");
                    input--;
                    view.Display("Enter Quantity: ");
                    quantity = UserInput.GetUserInputAsIntegerOrReturnOne("");


                    if (myOrder.PurchaseList.Contains(productList[input]))
                    {
                        myOrder.PurchaseList.Find(x => x.Name == productList[input].Name).Qty += quantity;
                    }
                    else
                    {

                        myOrder.PurchaseList.Add(productList[input]);
                        myOrder.PurchaseList.Find(x => x.Name == productList[input].Name).Qty += quantity;

                    }

                    break;

                default:

                    break;

            }

        }

        public void RemoveProductFromCart()
        {
            IView productListView = new ProductListView(myOrder.PurchaseList);
            productListView.Display();
            int input = UserInput.GetUserInputAsInteger("");
            int quantity = UserInput.GetUserInputAsInteger("");
            for (int i = 0; i < quantity; i++)
            {
                myOrder.PurchaseList.Remove(productList[input]);
                myOrder.Subtotal -= productList[input].Price;
            }
            myOrder.Tax = myOrder.Subtotal * Tax.tax;
        }

        public void ProcessPayment()
        {
            IMessage view = new MessageView();
            //Calculate Subtotal, tax and total
            myOrder.Subtotal = 0;
            foreach (Product p in myOrder.PurchaseList)
            {
                myOrder.Subtotal += p.Price * p.Qty;
            }
            myOrder.Tax = myOrder.Subtotal * Tax.tax;
            myOrder.Total = myOrder.Subtotal + myOrder.Tax;

            //Display the information
            IView paymentView = new PaymentView(myOrder);
            paymentView.Display();

            //ask how they want to pay
            view.Display("Input payment type: ");
            int input = UserInput.GetUserInputAsInteger("");
            switch (input)
            {
                case 1: //Cash - Get amount tendered and calculate change
                    myOrder.PayInfo.PayType = "cash";                    
                    
                    do
                    {
                        view.Display($"{myOrder.PayInfo.AmountTenderedMessage}");
                        myOrder.PayInfo.AmountTendered += UserInput.GetUserInputAsDouble("");
                        myOrder.PayInfo.Change = myOrder.PayInfo.AmountTendered - myOrder.Total;

                    } while (myOrder.PayInfo.AmountTendered < myOrder.Total);

                    view.Display($"{myOrder.PayInfo.ChangeMessage} {myOrder.PayInfo.Change:C2}");
                    break;
                case 2: //Credit - ask for number, expiration and cvv
                    myOrder.PayInfo.PayType = "credit";
                    view.Display(myOrder.PayInfo.CreditCardNumberMessage);
                    myOrder.PayInfo.CardNumber = UserInput.GetCreditCardNumber();
                    view.Display(myOrder.PayInfo.ExpirationDateMessage);
                    myOrder.PayInfo.ExpirationDate = UserInput.GetCreditCardExpiration();
                    view.Display(myOrder.PayInfo.CvvMessage);
                    myOrder.PayInfo.Cvv = UserInput.GetCreditCardCVV();
                    view.Display("Enter cash back (press enter or input 0 to skip): ");
                    myOrder.PayInfo.CashBack = UserInput.GetCashBack();
                    if (myOrder.PayInfo.CashBack != 0)
                    {
                        view.Display($"{myOrder.PayInfo.CashBackMessage} {myOrder.PayInfo.CashBack}");
                    }
                    break;
                case 3://Check - ask for check number
                    myOrder.PayInfo.PayType = "check";
                    view.Display(myOrder.PayInfo.CheckNumberMessage);
                    myOrder.PayInfo.CheckNumber = UserInput.GetCheckNumber();
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            ReceiptView receiptView = new ReceiptView(myOrder);
            receiptView.Display();
        }

    }

}