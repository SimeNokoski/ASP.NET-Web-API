using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Enums;
using MovieApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace MovieApp.DataAccess
{
    public class MovieAppDbContext : DbContext
    { 
        public MovieAppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
               .HasOne(x => x.User)
               .WithMany(x => x.MovieList)
               .HasForeignKey(x => x.UserId);



            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sime"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<User>()
                .HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Sime",
                    LastName = "Nokoski",
                    Username = "sime123",
                    Password = hashedPassword
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Rozeta",
                    LastName = "Markoska",
                    Username = "rozeta123",
                    Password = hashedPassword
                });

            modelBuilder.Entity<Movie>()
               .Property(x => x.Year)
               .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Movie>()
                .HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "Home Alone",
                    Description = "Home Alone",
                    Genre = GenreEnum.Action,
                    Year = 1988,
                    UserId = 1
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Borat",
                    Description = "Borat ",
                    Genre = GenreEnum.Comedy,
                    Year = 2006,
                    UserId = 1
                }
                );
        }
    }
}
