using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp.DataAccess.Implementation
{
    public class WinningTicketRepository : IWinningTicketRepository
    {
        private readonly LotoAppDbContext _dbContext;
        public WinningTicketRepository(LotoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(WinningTicket entity)
        {
            _dbContext.WinningTickets.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(WinningTicket entity)
        {
            _dbContext.WinningTickets.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<WinningTicket> GetAll()
        {
           return _dbContext.WinningTickets.ToList();
        }

        public WinningTicket GetById(int id)
        {
            return _dbContext.WinningTickets.SingleOrDefault(x => x.Id == id);
        }

        public void Update(WinningTicket entity)
        {
            _dbContext.WinningTickets.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
