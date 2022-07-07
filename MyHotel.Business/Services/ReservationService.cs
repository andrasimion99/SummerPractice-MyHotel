using AutoMapper;
using MyHotel.Business.Models;
using MyHotel.Domain.IRepositories;
using MyHotel.Entities;

namespace MyHotel.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRespository _reservationRespository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRespository reservationRespository, IMapper mapper)
        {
            _reservationRespository = reservationRespository;
            _mapper = mapper;
        }
        public int AddReservation(ReservationModel reservationModel)
        {
            if (_reservationRespository.CheckAvailability(_mapper.Map<Reservation>(reservationModel)))
            {
                var res = _reservationRespository.Add(_mapper.Map<Reservation>(reservationModel));
                return res.Id;
            }
            throw new System.Exception();
        }

        public bool CheckReservation(ReservationModel reservationModel)
        {
            return _reservationRespository.CheckAvailability(_mapper.Map<Reservation>(reservationModel));
        }
    }
}
