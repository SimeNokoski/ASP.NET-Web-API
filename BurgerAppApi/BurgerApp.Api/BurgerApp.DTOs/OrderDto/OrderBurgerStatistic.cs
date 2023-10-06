using BurgerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DTOs.OrderDto
{
    public class OrderBurgerStatistic
    {
        public string MostPopularBurger { get; set; }
        public int TotalOrders { get; set; }
        public List<string>Locations { get; set; }
        public double AveragePriceOrders { get; set; }
    }
}
