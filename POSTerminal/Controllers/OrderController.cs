﻿using System;
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
                productList = FileIO.GetFromProductFile();
                myOrder = new Order();
                myOrder.OrderNumber = FileIO.GetLastOrderNumber() + 1;

                menuView.Display();
                int input = UserInput.GetUserInputAsInteger("");

                switch (input)
                {
                    case 1:
                        CheckoutAction();
                        break;
                    case 2: SalesHistoryAction();
                        break;

                    case 3:
                        AddToProductFile();
                        break;

                    case 4:
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

        private void SalesHistoryAction()
        {
            List<Order> todayOrder = FileIO.GetFromOrderFileByDate(DateTime.Today.Date);

            foreach (Order order in todayOrder)
            {
                new OrderListView(order).ShowOrderDetail();
            }

            Console.ReadLine();
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

            view.Display("1: Search By Name\n2: Search By Category\n3: List All Products\n4: Go Back\n\nMake a selection: ");

            int input = UserInput.GetUserInputAsInteger("");
            bool goBack = false;

            while (!goBack)
            {


                switch (input)
                {
                    case 1:
                        goBack = true;

                        ProductListView filteredList;

                        do
                        {


                            view.Display("Enter product name: ");
                            string name = UserInput.GetUserInput("");
                            filteredList = new ProductListView(productListView.GetFilteredList("name", name));
                        }
                        while (filteredList.ProductList.Count < 1);

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
                    for (int i = 0; i < quantity; i++)
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

                    myOrder.PayInfo.PayType = POSTerminal.PayType.Cash;

                    do
                    {
                        view.Display($"{myOrder.PayInfo.AmountTenderedMessage}");
                        myOrder.PayInfo.AmountTendered += UserInput.GetUserInputAsDouble("");
                        myOrder.PayInfo.Change = myOrder.PayInfo.AmountTendered - myOrder.Total;

                    } while (myOrder.PayInfo.AmountTendered < myOrder.Total);

                    view.Display($"{myOrder.PayInfo.ChangeMessage} {myOrder.PayInfo.Change:C2}");
                    break;
                case 2: //Credit - ask for number, expiration and cvv
                    string cancel = string.Empty;
                    do
                    {
                        myOrder.PayInfo.PayType = POSTerminal.PayType.Credit;
                        view.Display(myOrder.PayInfo.CreditCardNumberMessage);
                        myOrder.PayInfo.CardNumber = UserInput.GetCreditCardNumber().Replace(" ", "");
                        view.Display(myOrder.PayInfo.ExpirationDateMessage);
                        myOrder.PayInfo.ExpirationDate = UserInput.GetCreditCardExpiration();
                        if (myOrder.PayInfo.ExpirationDate.Year >= DateTime.Now.Year)
                        {
                            if (myOrder.PayInfo.ExpirationDate.Month < DateTime.Now.Month)
                            {
                                view.Display("This credit card has expired.");
                                view.Display("Enter r to re-enter the credit card information, enter c to cancel the order: ");
                                cancel = UserInput.GetUserInput("");
                                if(cancel.ToLower() != "r")
                                {
                                    return;
                                }

                            }
                        }
                    } while (cancel.ToLower() == "r");
                    
                    view.Display(myOrder.PayInfo.CvvMessage);
                    myOrder.PayInfo.Cvv = UserInput.GetCreditCardCVV();
                    view.Display("Enter cash back (press enter or input 0 to skip): ");
                    myOrder.PayInfo.CashBack = UserInput.GetCashBack();
                    myOrder.PayInfo.AmountTendered = myOrder.Total + myOrder.PayInfo.CashBack;
                    if (myOrder.PayInfo.CashBack != 0)
                    {
                        view.Display($"{myOrder.PayInfo.CashBackMessage} {myOrder.PayInfo.CashBack}");
                    }
                    break;
                case 3://Check - ask for check number
                    myOrder.PayInfo.PayType = POSTerminal.PayType.Check;
                    view.Display(myOrder.PayInfo.CheckNumberMessage);
                    myOrder.PayInfo.CheckNumber = UserInput.GetCheckNumber().Replace(" ", "");
                    myOrder.PayInfo.AmountTendered = myOrder.Total;
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            ReceiptView receiptView = new ReceiptView(myOrder);
            FileIO.WriteToOrderFile(myOrder);
            receiptView.Display();
        }

        public void AddToProductFile()
        {
            Product newProduct = new Product();
            IView addProductView = new AddProductView(productList);
            IMessage view = new MessageView();
            addProductView.Display();            
            view.Display("\nPlease enter EXIT at any time to quit.\n");
            view.Display("Enter new product name: ");
            string input = UserInput.GetUserInput("");

            if(input.ToLower() == "exit")
            {
                return;
            }
            newProduct.Name = input;
            view.Display("Enter new product category: ");
            input = UserInput.GetUserInput("");
            if(input.ToLower() == "exit")
            {
                return;
            }
            newProduct.Category = input;
            bool checkPrice = false;
            while (!checkPrice)
            {
                view.Display("Enter new product price: ");
                input = UserInput.GetUserInput("");
                if (input.ToLower() == "exit")
                {
                    return;
                }
                if (double.TryParse(input, out double newPrice))
                {
                    newProduct.Price = newPrice;
                    checkPrice = true;
                }
            }
            view.Display("Enter new product description: ");
            input = UserInput.GetUserInput("");
            if(input.ToLower() == "exit")
            {
                return;
            }
            newProduct.Description = input;
                        
            while(input.ToLower() != "y" && input.ToLower() != "n")
            {
                view.Display($"\nName: {newProduct.Name}\nCategory: {newProduct.Category}\nPrice: {newProduct.Price}\nDescription: {newProduct.Description}");
                view.Display("\nEnter this product into the database? (y/n): ");
                input = UserInput.GetUserInput("");
            }

            bool alreadyExists = false;
            if(input.ToLower() == "y")
            {
                foreach(Product p in productList)
                {
                    if(p.Name.ToLower() == newProduct.Name.ToLower())
                    {
                        view.Display("A product with this name already exists!\n");
                        view.Display("This new product will not be added to the database!");
                        Console.ReadKey();
                        alreadyExists = true;
                    }
                }

                if(!alreadyExists)
                {
                    FileIO.WriteToProductFile(newProduct);
                    Console.WriteLine("Wrote to File!");
                    Console.ReadKey();
                }
            }
            else
            {
                view.Display("The new product was not added to the database.");
            }

        }

    }

}