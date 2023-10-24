using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace LotoApp.DataAccess
{
    public class LotoAppDbContext : DbContext
    {
        public LotoAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<WinningTicket> WinningTickets { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("sime123!"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "Sime",
                    LastName = "Nokoski",
                    Password = hashedPassword,
                    Role = "superAdmin",
                    UserName = "sime123",
                });

        }

    }
}
