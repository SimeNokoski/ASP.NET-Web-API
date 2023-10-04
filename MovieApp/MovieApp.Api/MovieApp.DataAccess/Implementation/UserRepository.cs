using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Interfaces;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieAppDbContext _dbContext;
        public UserRepository(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.Include(x => x.MovieList);
        }

        public User GetById(int id)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Id == id);
        }

        public void Update(User entity)
        {
           _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
