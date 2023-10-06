using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Shared.BurgerException
{
    public class BurgerException : Exception
    {
        public BurgerException(string  message) : base(message) { }
    }
}
