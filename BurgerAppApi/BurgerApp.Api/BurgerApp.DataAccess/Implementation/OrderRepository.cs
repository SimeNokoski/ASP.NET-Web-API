using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DataAccess.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BurgerAppDbContext _dbContext;
        public OrderRepository(BurgerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Order entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _dbContext
                .Orders
                .Include(x=>x.BurgerOrders)
                .ThenInclude(x=>x.Burger)
                .Include(x=>x.User)
                .Include(x=>x.Location)
                .ToList();
        }

        public Order GetById(int id)
        {
            return _dbContext
                .Orders
                .Include(x => x.BurgerOrders)
                .ThenInclude(x => x.Burger)
                .Include(x => x.User)
                .Include(x=>x.Location)
                .SingleOrDefault(x => x.Id == id);
        }

        public void Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
