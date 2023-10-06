using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DataAccess.Implementation
{
    public class LocationRepository : ILocationRepository
    {
        private readonly BurgerAppDbContext _dbContext;
        public LocationRepository(BurgerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Location entity)
        {
            _dbContext.Locations.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Location entity)
        {
            _dbContext.Locations.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Location> GetAll()
        {
           return _dbContext.Locations.ToList();
        }

        public Location GetById(int id)
        {
            return _dbContext.Locations.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Location entity)
        {
            _dbContext.Locations.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
