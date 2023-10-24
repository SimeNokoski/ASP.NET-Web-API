using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.Shared
{
    public class WinningTicketNotFoundException : Exception
    {
        public WinningTicketNotFoundException(string message) : base(message) { }
    }
}
