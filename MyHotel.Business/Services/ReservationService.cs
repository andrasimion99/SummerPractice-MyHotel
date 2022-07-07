using System;
using System.Linq;
using AutoMapper;
using MyHotel.Business.Exceptions;
using MyHotel.Business.Models;
using MyHotel.Domain.IRepositories;
using MyHotel.Entities;

namespace MyHotel.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }
        public Guid AddReservation(ReservationModel reservationModel)
        {
            if (_reservationRepository.CheckAvailability(_mapper.Map<Reservation>(reservationModel), out var rooms))
            {
                var reservation = _mapper.Map<Reservation>(reservationModel);
                reservation.Rooms = rooms;

                var res = _reservationRepository.Add(reservation);
                return res.Id;
            }
            throw new NotAvailableException("The reservation can't be fulfilled anymore");
        }

        public bool CheckReservation(ReservationModel reservationModel)
        {
            return _reservationRepository.CheckAvailability(_mapper.Map<Reservation>(reservationModel), out _);
        }
    }
}
