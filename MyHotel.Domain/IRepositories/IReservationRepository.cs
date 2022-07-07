using System.Collections.Generic;
using MyHotel.Entities;

namespace MyHotel.Domain.IRepositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        bool CheckAvailability(Reservation newReservation, out IList<Room> rooms);
    }
}
