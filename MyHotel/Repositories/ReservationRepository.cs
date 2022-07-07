using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using System.Linq;
using MyHotel.Domain.Entities;
using MyHotel.Persistance.Data;

namespace MyHotel.Persistance.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRespository
    {
        public ReservationRepository(MyHotelDbContext myHotelDbContext): base(myHotelDbContext)
        {

        }

        public bool CheckAvailability(Reservation newReservation)
        {
            var roomsReserved = _dbContext.Reservations.Where(res => res.CheckIn <= newReservation.CheckIn && res.CheckOut > newReservation.CheckIn)
                                   .Where(res => res.CheckIn >= newReservation.CheckIn && res.CheckIn < newReservation.CheckOut)
                                   .SelectMany(res => res.Rooms).ToList();
            var availableRooms = _dbContext.Rooms.ToList().Except(roomsReserved);
            if (availableRooms.Count() < newReservation.Rooms.Count())
                return false;

            var roomCapacityNeeded = newReservation.Rooms.GroupBy(x => x.Capacity);
            foreach(var roomCapacity in roomCapacityNeeded)
            {
                //itereate through keys of the group by
               
                if (availableRooms.Count(x => x.Capacity == roomCapacity.Key) < roomCapacity.Count())
                    return false;
            }
            return true;
        }
    }
}
