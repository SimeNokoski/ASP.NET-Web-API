using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Shared.LocationException
{
    public class LocationException : Exception
    {
        public LocationException(string message) : base(message) { }
    }
}
