using System;
using System.Collections.Generic;
using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyHotel.Persistance.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(MyHotelDbContext myHotelDbContext): base(myHotelDbContext)
        {

        }

        public IEnumerable<Room> GetAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            var roomsReserved = _dbContext.Reservations.Where(res => res.CheckIn <= checkIn && res.CheckOut > checkIn)
                .Where(res => res.CheckIn >= checkIn && res.CheckIn < checkOut)
                .SelectMany(res => res.Rooms)
                .ToList();
            return _dbContext.Rooms
                .ToList()
                .Except(roomsReserved);
        }
    }
}
