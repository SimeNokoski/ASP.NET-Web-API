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
    public class SessionRepository : ISessionRepository
    {
        private readonly LotoAppDbContext _db;
        public SessionRepository( LotoAppDbContext db )
        {
            _db = db;
        }

        public Session ActiveSession()
        {
            return _db.Sessions.Where(x => x.Active).FirstOrDefault();
        }

        public void Add(Session entity)
        {
            _db.Sessions.Add( entity );
            _db.SaveChanges();
        }

        public void Delete(Session entity)
        {
            _db.Sessions.Remove(entity);
            _db.SaveChanges();
        }

        public List<Session> GetAll()
        {
            return _db.Sessions.Include(x=>x.Tickets).ThenInclude(x=>x.User).Include(x=>x.WinningTicket).ToList();      
        }

        public Session GetById(int id)
        {
            return _db.Sessions.Include(x => x.Tickets).ThenInclude(x => x.User).Include(x => x.WinningTicket).SingleOrDefault(x => x.Id == id);
        }

        public void Update(Session entity)
        {
            _db.Sessions.Update( entity );
            _db.SaveChanges();
        }
    }
}
