using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTerminal
{
    class MessageView : IMessage
    {
        public void Display(string message)
        {
            Console.Write(message);
        }
    }
}