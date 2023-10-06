using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.Domain.Models
{
    public class Order : BaseEntity
    {
        public Location Location { get; set; }  
        public int LocationId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsDelivered { get; set; }
        public List<BurgerOrder>? BurgerOrders { get; set; }
    }
}
