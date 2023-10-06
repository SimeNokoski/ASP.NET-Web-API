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
    public class BurgerRepository : IBurgerRepository
    {
        private readonly BurgerAppDbContext _dbContext;
        public BurgerRepository(BurgerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Burger entity)
        {
            _dbContext.Burgers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Burger entity)
        {
            _dbContext.Burgers.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Burger> GetAll()
        {
            return _dbContext.Burgers.ToList();
        }

        public Burger GetById(int id)
        {
            return _dbContext.Burgers.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Burger entity)
        {
            _dbContext.Burgers.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
