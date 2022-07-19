using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHotel.Business.Models;
using MyHotel.Domain.Entities;

namespace MyHotel.IntegrationTests
{
    [TestClass]
    public class RoomControllerTests : BaseIntegrationTests
    {
        [TestMethod]
        public async Task AddRoom_ShouldInsertRoom()
        {
            // Arrange
            var roomModel = new AddRoomModel
            {
                RoomNumber = 1,
                Capacity = 10,
                Price = 200,
                Facilities = "Room service",
                Status = "available"
            };

            // Act
            var addRoomResponse = await HttpClient.PostAsJsonAsync("api/room", roomModel);

            // Assert
            addRoomResponse.EnsureSuccessStatusCode();
            var roomId = await addRoomResponse.Content.ReadAsStringAsync();
            roomId.Should().NotBeNullOrEmpty();

            var roomsResponse = await HttpClient.GetAsync($"api/room/{roomId}");
            roomsResponse.EnsureSuccessStatusCode();
            var room = await roomsResponse.Content.ReadFromJsonAsync<Room>();
            room.Should().NotBeNull();
            room.Id.ToString().Should().Be(roomId);
        }
    }
}