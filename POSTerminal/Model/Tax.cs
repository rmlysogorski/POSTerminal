using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    public sealed class Tax
    {
        private Tax()
        {            
        }

        public static double tax
        {
            get { return 0.06; }
        }

    }
}
