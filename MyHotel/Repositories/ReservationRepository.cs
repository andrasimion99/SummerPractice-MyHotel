using System;
using System.Collections.Generic;
using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using MyHotel.Entities;
using System.Linq;

namespace MyHotel.Persistance.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(MyHotelDbContext myHotelDbContext): base(myHotelDbContext)
        {

        }

        public bool CheckAvailability(Reservation newReservation, out IList<Room> rooms)
        {
            rooms = new List<Room>();
            var availableRooms = GetAvailableRooms(newReservation.CheckIn, newReservation.CheckOut).ToList();

            if (availableRooms.Count < newReservation.Rooms.Count)
                return false;
            
            var roomCapacityNeeded = newReservation.Rooms.GroupBy(x => x.Capacity);
            foreach(var roomCapacity in roomCapacityNeeded)
            {
                //itereate through keys of the group by
               
                if (availableRooms.Count(x => x.Capacity == roomCapacity.Key) < roomCapacity.Count())
                    return false;
            }

            rooms = availableRooms;
            return true;
        }

        private IEnumerable<Room> GetAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            var roomsReserved = _dbContext.Reservations.Where(res => res.CheckIn <= checkIn && res.CheckOut > checkIn)
                .Where(res => res.CheckIn >= checkIn && res.CheckIn < checkOut)
                .SelectMany(res => res.Rooms).ToList();
            return _dbContext.Rooms.ToList().Except(roomsReserved);
        }
    }
}
