using MyHotel.Business.Models;
using System;

namespace MyHotel.Business.Services
{
    public interface IReservationService
    {
        Guid AddReservation(ReservationModel reservationModel);
        bool CheckReservation(ReservationModel reservationModel);
    }
}
