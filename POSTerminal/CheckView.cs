using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class CheckView : IView
    {

        private string checkNumber;

        public string CheckNumber
        {
            get { return checkNumber; }
            set { checkNumber = value; }
        }

        public CheckView()
        {

        }
        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
