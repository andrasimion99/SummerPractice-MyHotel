using MyHotel.Business.Models;
using System;

namespace MyHotel.Business.Services
{
    public interface IReservationService
    {
        int AddReservation(ReservationModel reservationModel);
        bool CheckReservation(ReservationModel reservationModel);
    }
}
