using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Domain.Models
{
    public class BurgerOrder : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int BurgerId { get; set; }
        public Burger Burger { get; set; }
    }
}
