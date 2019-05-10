using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class AddProductView : IView
    {
        private List<Product> productList;

        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; }
        }

        public AddProductView(List<Product> products)
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
