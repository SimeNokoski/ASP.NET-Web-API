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
    public class UserRepository : IUserRepository
    {
        private readonly BurgerAppDbContext _dbContext;
        public UserRepository(BurgerAppDbContext dbContext)
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

        public List<User> GetAll()
        {
           return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower()
            && x.Password == hashedPassword);
        }

        public void Update(User entity)
        {
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
