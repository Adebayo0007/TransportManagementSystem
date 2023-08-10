using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using TrainStationManagementApplication.Models.Entities;
using Route = TrainStationManagementApplication.Models.Entities.Route;
using Transaction = TrainStationManagementApplication.Models.Entities.Transaction;
namespace TrainStationManagementApplication.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var address = new Address
        //    {
        //        Id = "b13c100d/9",
        //        Street = "Odoeran street",
        //        City = "Obantoko",
        //        State = "Ogun",
        //        Country = "Nigeria",

        //    };

        //    modelBuilder.Entity<Address>().HasData(address);

        //    var user = new User
        //    {
        //        Id = "3c11b976/2",
        //        Address = address,
        //        Role = "Admin",
        //        FirstName = "Ebuka",
        //        LastName = "Anthony",
        //        Email = "ebukaanthony9@gmail.com",
        //        Password = BCrypt.Net.BCrypt.HashPassword("ebuka1234"),
        //        Image = "~/upload/89ed328b615d456f8441fa09ecace86f.jpg",
        //        Gender = Models.Enums.Gender.Male,
        //        PhoneNumber = "08161778965",
        //        DateCreated = DateTime.UtcNow,
        //        IsDeleted = false,
        //    };

        //    modelBuilder.Entity<User>().HasData(user);

        //    var admin = new Admin
        //    {
        //        Id = "2c094001/3",
        //        UserId = "3c11b976/2",
        //        User = user,
        //        StaffNumber = "STF78837C84",
        //    };

        //    modelBuilder.Entity<Admin>().HasData(admin);

        //}
    }


}
