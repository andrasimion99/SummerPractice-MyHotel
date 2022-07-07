using System;
using System.Collections.Generic;
using MyHotel.Entities;

namespace MyHotel.Domain.IRepositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        IEnumerable<Room> GetAvailableRooms(DateTime checkIn, DateTime checkOut);
    }
}
