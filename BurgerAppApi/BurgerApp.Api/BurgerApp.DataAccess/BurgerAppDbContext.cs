using BurgerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace BurgerApp.DataAccess
{
    public class BurgerAppDbContext : DbContext
    {
        public BurgerAppDbContext(DbContextOptions options) : base(options) { } 
        public DbSet<User> Users { get; set; }
        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
            .HasMany(x => x.BurgerOrders)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<Burger>()
               .HasMany(x => x.BurgerOrders)
               .WithOne(x => x.Burger)
               .HasForeignKey(x => x.BurgerId);

            modelBuilder.Entity<User>()
               .HasMany(x => x.Orders)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Location>()
                 .HasMany(x=>x.Orders)
                 .WithOne(x=>x.Location)
                 .HasForeignKey(x => x.LocationId);

            modelBuilder.Entity<Burger>()
              .HasData(
              new Burger
              {
                  Id = 1,
                  Name = "Hamburger",
                  IsVegan = true,
                  Price = 180,
                  IsVegetarian = true,
                  HasFries = false

              },
              new Burger
              {
                  Id = 2,
                  Name = "Cheeseburger",
                  Price = 200,
                  IsVegetarian = true,
                  IsVegan = false,
                  HasFries = true

              },
              new Burger
              {
                  Id = 3,
                  Name = "Baconburger",
                  Price = 300,
                  IsVegan = false,
                  IsVegetarian = false,
                  HasFries = false

              },
              new Burger
              {
                  Id = 4,
                  Name = "MegaBurger",
                  Price = 400,
                  IsVegetarian = false,
                  IsVegan = false,
                  HasFries = true
              });

            var md5 = new MD5CryptoServiceProvider();
            var md5data1 = md5.ComputeHash(Encoding.ASCII.GetBytes("sime123!"));
            var hashedPassword1 = Encoding.ASCII.GetString(md5data1);
            var md5data2 = md5.ComputeHash(Encoding.ASCII.GetBytes("marko123!"));
            var hashedPassword2 = Encoding.ASCII.GetString(md5data2);
            var md5data3 = md5.ComputeHash(Encoding.ASCII.GetBytes("rozeta123!"));
            var hashedPassword3 = Encoding.ASCII.GetString(md5data3);

            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Sime",
                    LastName = "Nokoski",
                    UserName = "sime123!",
                    Password = hashedPassword1,
                    Address = "Trpe Blazeski",
                    Role = "superAdmin"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Marko",
                    LastName = "Markoski",
                    UserName= "marko123",
                    Password = hashedPassword2,
                    Address = "Rudnicka",
                    Role = "customer"

                },
                new User
                {
                    Id = 3,
                    FirstName = "Rozeta",
                    LastName = "Markoska",
                    Password = hashedPassword3,
                    UserName = "rozeta123",
                    Address = "8Septemvri",
                    Role = "customer"

                });

            modelBuilder.Entity<Order>()
                .HasData(
                new Order
                {
                    Id = 1,
                    LocationId = 1,
                    UserId = 1,
                    
                },
                new Order
                {
                    Id = 2,
                   LocationId = 2,
                    UserId = 3,
                });

            modelBuilder.Entity<BurgerOrder>()
                .HasData(
                new BurgerOrder
                {
                    Id = 1,
                    BurgerId = 1,
                    OrderId = 1,
                },
                new BurgerOrder
                {
                    Id = 2,
                    BurgerId = 4,
                    OrderId = 1,
                },
                new BurgerOrder
                {
                    Id = 3,
                    BurgerId = 2,
                    OrderId = 2,
                });

            modelBuilder.Entity<Location>()
                .HasData(new Location
                {
                    Address = "kicevo",
                    Name = "kicburger",
                    Id = 1,
                    OpenAt = DateTime.Today.AddHours(8),
                    CloseAt = DateTime.Today.AddHours(22),
                });;

            modelBuilder.Entity<Location>()
            .HasData(new Location
            {
                Address = "bitola",
                Name = "bitburger",
                Id = 2,
                OpenAt = DateTime.Today.AddHours(8),
                CloseAt = DateTime.Today.AddHours(22),
            });
        }

    }
}
