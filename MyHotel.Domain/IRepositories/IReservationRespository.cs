﻿using MyHotel.Domain.Entities;

namespace MyHotel.Domain.IRepositories
{
    public interface IReservationRespository : IBaseRepository<Reservation>
    {
        bool CheckAvailability(Reservation newReservation);
    }
}