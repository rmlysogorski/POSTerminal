using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class ProductListView : IView
    {
        private List<Product> productList;

        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; }
        }

        public ProductListView(List<Product> products)
        {
            productList = products;
        }

        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
