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
            bool repeat = true;

            while (repeat)
            {
                menuView.Display();
                int input = UserInput.GetUserInputAsInteger("");

                switch (input)
                {
                    case 1:
                        CheckoutAction();
                        break;
                    case 2: //SalesHistoryAction();
                        break;

                    case 3:
                        new MessageView().Display("Are you sure (Y/N)? ");

                        if (UserInput.UserConfirmationPrompt(""))
                        {
                            repeat = false;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid Selection");
                        break;
                }
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
            view.Display("1: Search By Name\n2: Search By Category\n3: List All Products\n4: Go Back\n\nMake a selection: ");
            int input = UserInput.GetUserInputAsInteger("");
            bool goBack = false;

            while (!goBack)
            {

                switch (input)
                {
                    case 1:
                        goBack = true;
                        view.Display("Enter product name: ");
                        string name = UserInput.GetUserInput("");
                        ProductListView filteredList = new ProductListView(productListView.GetFilteredList("name", name));

                        filteredList.Display();
                        view.Display(string.Format("\nChoose a product (1-{0}): ", filteredList.ProductList.Count));
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
                            filteredList.ProductList[input].Qty += quantity;
                            myOrder.PurchaseList.Add(filteredList.ProductList[input]);
                        }

                        break;
                    case 2:
                        goBack = true;
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
                        goBack = true;
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

                    case 4:
                        goBack = true;
                        break;

                    default:

                        break;

                }
            }

        }

        public void RemoveProductFromCart()
        {
            if (myOrder.PurchaseList.Count > 0)
            {
                IView productListView = new ProductListView(myOrder.PurchaseList);
                productListView.Display();

                new MessageView().Display($"Enter item#(1-{myOrder.PurchaseList.Count}): ");
                int input = UserInput.GetUserInputAsInteger("");
                
                while (input <= 0 || input > myOrder.PurchaseList.Count)
                {
                    input = UserInput.GetUserInputAsInteger("");
                }
                input--;
                new MessageView().Display("Quantity(Press ENTER to delete 1): ");
                int quantity = UserInput.GetUserInputAsIntegerOrReturnOne("");
                string nameOfItem = myOrder.PurchaseList[input].Name;
                Product deletedProduct = myOrder.PurchaseList.Find(x => x.Name == nameOfItem);

                if (deletedProduct.Qty == quantity)
                {
                    for(int i =0; i < quantity; i++)
                    {
                        myOrder.Subtotal -= deletedProduct.Price;
                    }
                    myOrder.PurchaseList.Remove(deletedProduct);                    
                }
                else
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        deletedProduct.Qty--;
                        myOrder.Subtotal -= deletedProduct.Price;
                    }
                }
                myOrder.Tax = myOrder.Subtotal * Tax.tax;
            }
            else
            {
                new MessageView().Display("No items in current checkout....\n\n");
            }
        }

        public void ProcessPayment()
        {
            myOrder.Total = myOrder.Subtotal + myOrder.Tax;
            IView paymentView = new PaymentView(myOrder);
            paymentView.Display();
            int input = UserInput.GetUserInputAsInteger("");
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