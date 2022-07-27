using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHotel.Business.Exceptions;
using MyHotel.Business.Models;
using MyHotel.Business.Services;
using MyHotel.Domain.Entities;
using MyHotel.Domain.IRepositories;

namespace MyHotel.Business.Tests.Services
{
    [TestClass]
    public class ReservationServiceTests
    {
        private Mock<IReservationRepository> _reservationRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private List<Room> _availableRooms;

        [TestInitialize]
        public void TestInitialize()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _mapperMock = new Mock<IMapper>();

            _availableRooms = new List<Room>
            {
                new() { Capacity = 4 }, new() { Capacity = 4 }, new() { Capacity = 4 }
            };
        }

        [TestMethod]
        public void AddReservation_ShouldAddReservation_WhenAvailableRoomsCoverRequestedRooms()
        {
            // Arrange
            var expectedRooms = new List<Room> { new() { Capacity = 4 }, new() { Capacity = 6 } };
            _availableRooms = new List<Room> { new() { Capacity = 2 } };
            _availableRooms.AddRange(expectedRooms);
            var model = new ReservationModel
            {
                Rooms = new List<RoomModel> { new() { Capacity = 4}, new() { Capacity = 6 } }
            };
            var reservation = new Reservation
            {
                Id = 1,
                Rooms = model.Rooms.Select(rm => new Room { Capacity = rm.Capacity }).ToList()
            };
            _reservationRepositoryMock
                .Setup(r => r.GetAvailableRooms(model.CheckIn, model.CheckOut))
                .Returns(_availableRooms);
            _reservationRepositoryMock
                .Setup(r => r.Add(It.IsAny<Reservation>()))
                .Returns(reservation);
            _mapperMock
                .Setup(m => m.Map<Reservation>(model))
                .Returns(reservation);

            var sut = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = sut.AddReservation(model);

            // Assert
            _reservationRepositoryMock.Verify(r => r.Add(reservation), Times.Once);
            reservation.Rooms.Should().BeEquivalentTo(expectedRooms);
            result.Should().Be(1);
        }

        [TestMethod]
        public void AddReservation_ShouldNotAddReservation_WhenRequestedRoomsExceedAvailableRooms()
        {
            // Arrange
            var model = new ReservationModel
            {
                Rooms = new List<RoomModel>
                {
                    new() { Capacity = 4 }, new() { Capacity = 4 }, new() { Capacity = 8 }
                }
            };
            _reservationRepositoryMock
                .Setup(r => r.GetAvailableRooms(model.CheckIn, model.CheckOut))
                .Returns(_availableRooms);
            var sut = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object);

            // Act
            Action action = () => sut.AddReservation(model);

            // Assert
            action.Should().Throw<NotAvailableException>()
                .WithMessage("The reservation can't be fulfilled anymore");
            _reservationRepositoryMock
                .Verify(r => r.Add(It.IsAny<Reservation>()), Times.Never);
        }

        [TestMethod]
        public void CheckReservation_ShouldReturnFalse_WhenMoreRoomsThanAvailableAreRequested()
        {
            // Arrange
            var reservationModel = new ReservationModel
            {
                Rooms = new List<RoomModel>
                {
                    new() { Capacity = 1 }, new() { Capacity = 1 }, new() { Capacity = 1 }, new() { Capacity = 1 }
                }
            };
            _reservationRepositoryMock
                .Setup(r => r.GetAvailableRooms(reservationModel.CheckIn, reservationModel.CheckOut))
                .Returns(_availableRooms);
            var sut = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = sut.CheckReservation(reservationModel);

            // Assert
            result.Should().BeFalse();
        }
        
        [TestMethod]
        public void CheckReservation_ShouldReturnFalse_WhenRequestedCapacityExceedsAvailableCapacity()
        {
            // Arrange
            var model = new ReservationModel
            {
                Rooms = new List<RoomModel>
                {
                    new() { Capacity = 4 }, new() { Capacity = 4 }, new() { Capacity = 8 }
                }
            };
            _reservationRepositoryMock
                .Setup(r => r.GetAvailableRooms(model.CheckIn, model.CheckOut))
                .Returns(_availableRooms);
            var sut = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = sut.CheckReservation(model);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void CheckReservation_ShouldReturnFalse_WhenRequestedCapacityIsLessThanAvailableCapacity()
        {
            // Arrange
            var model = new ReservationModel
            {
                Rooms = new List<RoomModel>
                {
                    new() { Capacity = 4 }, new() { Capacity = 4 }, new() { Capacity = 2 }
                }
            };
            _reservationRepositoryMock
                .Setup(r => r.GetAvailableRooms(model.CheckIn, model.CheckOut))
                .Returns(_availableRooms);
            var sut = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = sut.CheckReservation(model);

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void CheckReservation_ShouldReturnTrue_WhenRequestedCapacityExactlyMatchesAvailableCapacity()
        {
            // Arrange
            var model = new ReservationModel
            {
                Rooms = new List<RoomModel>
                {
                    new() { Capacity = 4 }, new() { Capacity = 4 }
                }
            };
            _reservationRepositoryMock
                .Setup(r => r.GetAvailableRooms(model.CheckIn, model.CheckOut))
                .Returns(_availableRooms);
            var sut = new ReservationService(_reservationRepositoryMock.Object, _mapperMock.Object);

            // Act
            var result = sut.CheckReservation(model);

            // Assert
            result.Should().BeTrue();
        }
    }
}