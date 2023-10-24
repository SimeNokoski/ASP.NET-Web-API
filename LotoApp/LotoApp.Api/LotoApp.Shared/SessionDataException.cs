using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Shared
{
    public class SessionDataException : Exception
    {
        public SessionDataException(string message) : base(message) { } 
    }
}
