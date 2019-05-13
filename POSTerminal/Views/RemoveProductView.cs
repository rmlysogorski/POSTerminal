using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal.Views
{
    class RemoveProductView : IView
    {
        private List<Product> productList;

        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; }
        }

        public RemoveProductView(List<Product> products)
        {
            productList = products;
        }

        public void Display()
        {
            IView productListView = new ProductListView(productList);
            productListView.Display();


        }
    }
}
