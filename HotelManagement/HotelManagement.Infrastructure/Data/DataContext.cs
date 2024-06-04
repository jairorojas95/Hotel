
using Microsoft.EntityFrameworkCore;
using HotelManagement.Core.Entities;

namespace HotelBookingAPI.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Room> Room { get; set; }
     
  



        public DbSet<Passenger> Passenger { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
