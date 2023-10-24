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
    public class TicketRepository : ITicketRepository
    {
        private readonly LotoAppDbContext _context;
        public TicketRepository(LotoAppDbContext context)
        {
            _context = context;
        }

        public void Add(Ticket entity)
        {
            _context.Tickets.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            _context.Tickets.Remove(entity);
            _context.SaveChanges();
        }

        public List<Ticket> GetAll()
        {
           return _context.Tickets.Include(x=>x.User).Include(x=>x.Session).ThenInclude(x=>x.WinningTicket).ToList();
        }

        public Ticket GetById(int id)
        {
            return _context.Tickets.Include(x => x.User).SingleOrDefault(x => x.Id == id);
        }

        public void Update(Ticket entity)
        {
            _context.Tickets.Update(entity);
            _context.SaveChanges();
        }
    }
}
