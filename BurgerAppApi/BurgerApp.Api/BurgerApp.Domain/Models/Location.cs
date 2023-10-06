using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Domain.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime CloseAt { get; set; }
        public List<Order>Orders { get; set; }
    }
}
