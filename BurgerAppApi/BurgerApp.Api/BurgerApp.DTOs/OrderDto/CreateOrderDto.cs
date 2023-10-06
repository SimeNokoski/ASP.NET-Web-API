using BurgerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DTOs.OrderDto
{
    public class CreateOrderDto
    {

        public bool IsDelivered { get; set; }
        public int LocatioId { get; set; }
    }
}
