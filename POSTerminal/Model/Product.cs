using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class Product
    {
        private string name;
        private string category;
        private double price;
        private string description;
        private int qty;

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Category
        {
            set { category = value; }
            get { return category; }
        }

        public double Price
        {
            set { price = value; }
            get { return price; }
        }

        public string Description
        {
            set { description = value; }
            get { return description; }
        }

        public int Qty
        {
            set { qty = value; }
            get { return qty; }
        }
        public Product(string _name, string _category, double _price, string _description, int _qty = 1)
        {
            name = _name;
            price = _price;
            category = _category;
            description = _description;
            qty = _qty;
        }

    }
}
