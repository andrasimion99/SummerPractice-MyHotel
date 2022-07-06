using Microsoft.EntityFrameworkCore;
using MyHotel.Data.Mappings;
using MyHotel.Entities;

namespace MyHotel.Data
{
    public class MyHotelDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            GuestMapping.Map(modelBuilder);
            ReservationMapping.Map(modelBuilder);
            RoomMapping.Map(modelBuilder);
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
