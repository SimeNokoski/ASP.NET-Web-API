using BurgerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DTOs.OrderDto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserAddress { get; set; }
        public int UserId { get; set; }
        public bool IsDelivered { get; set; }
        public double TotalPrice { get; set; }
        public string UserFullName { get; set; }
        public List<string> Burgers { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }
      
    }
}
