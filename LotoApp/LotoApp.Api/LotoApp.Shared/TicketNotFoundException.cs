using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Shared
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string message) : base(message) { }
    }
}
