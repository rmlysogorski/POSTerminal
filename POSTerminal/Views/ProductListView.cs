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
            int count = 1;
            foreach(Product p in productList)
            {
                Console.WriteLine($"{count}: {p.Name} ({p.Price:C2})");
                count++;
            }
        }

        public List<Product> GetFilteredList(string code, string input)
        {
            if(code.ToLower() == "name")
            {
                return productList.FindAll(x => x.Name.Contains(input));
                
            }
            else if (code.ToLower() == "category")
            {

                return productList.FindAll(x => x.Category.Contains(input));
            }

            return new List<Product>();
        }
    }
}
