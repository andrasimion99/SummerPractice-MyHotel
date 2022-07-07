using AutoMapper;
using MyHotel.Business.Exceptions;
using MyHotel.Business.Models;
using MyHotel.Business.Services.IServices;
using MyHotel.Domain.Entities;
using MyHotel.Domain.IRepositories;

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
            throw new NotAvailableException("The reservation can't be fulfilled anymore");
        }

        public bool CheckReservation(ReservationModel reservationModel)
        {
            return _reservationRespository.CheckAvailability(_mapper.Map<Reservation>(reservationModel));
        }
    }
}
