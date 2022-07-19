using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHotel.Business.Models;

namespace MyHotel.IntegrationTests
{
    [TestClass]
    public class ReservationControllerTests : BaseIntegrationTests
    {
        [TestMethod]
        public async Task CheckAvailability_ShouldReturnTrue_WhenRoomIsAvailable()
        {
            // Arrange
            var addRoomModel = new AddRoomModel
            {
                RoomNumber = 1,
                Capacity = 10,
                Price = 200,
                Facilities = "Room service",
                Status = "available"
            };
            var addRoomResponse = await HttpClient.PostAsJsonAsync("api/Room", addRoomModel);
            addRoomResponse.EnsureSuccessStatusCode();

            var roomModel = new RoomModel
            {
                Capacity = 10
            };
            var reservationModel = new ReservationModel
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                ReservationDate = DateTime.Now,
                Rooms = new List<RoomModel>{ roomModel },
                Guest = new GuestModel{Email = "email@email.com", FirstName = "Stephen", LastName = "Green", PhoneNumber = "4567", SSN = "1234"}
            };

            // Act
            var checkAvailabilityResponse = await HttpClient.PostAsJsonAsync("api/Reservation/checkavailability", reservationModel);

            // Assert
            checkAvailabilityResponse.EnsureSuccessStatusCode();
            var areRoomsAvailable = await checkAvailabilityResponse.Content.ReadAsStringAsync();
            areRoomsAvailable.ToLower().Should().Be(true.ToString().ToLower());
        }

        [TestMethod]
        public async Task AddReservation_ShouldInsertReservation()
        {
            // Arrange
            var addRoomModel = new AddRoomModel
            {
                RoomNumber = 1,
                Capacity = 10,
                Price = 200,
                Facilities = "Room service",
                Status = "available"
            };
            var addRoomResponse = await HttpClient.PostAsJsonAsync("api/Room", addRoomModel);
            addRoomResponse.EnsureSuccessStatusCode();

            var roomModel = new RoomModel
            {
                Capacity = 10
            };
            var reservationModel = new ReservationModel
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                ReservationDate = DateTime.Now,
                Rooms = new List<RoomModel> { roomModel },
                Guest = new GuestModel { Email = "email@email.com", FirstName = "Stephen", LastName = "Green", PhoneNumber = "4567", SSN = "1234" }
            };

            // Act
            var addReservationResponse = await HttpClient.PostAsJsonAsync("api/reservation", reservationModel);

            // Assert
            addReservationResponse.EnsureSuccessStatusCode();
            var reservationId = await addReservationResponse.Content.ReadAsStringAsync();
            reservationId.Should().NotBeNullOrEmpty();
        }
    }
}