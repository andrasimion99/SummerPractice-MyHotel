using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHotel.Business.Services;
using MyHotel.Domain.Entities;
using MyHotel.Domain.IRepositories;

namespace MyHotel.Business.Tests.Services
{
    [TestClass]
    public class RoomServiceTests
    {
        private Mock<IRoomRepository> _roomRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [TestMethod]
        public void DeleteRoom_ShouldDeleteRoom_WhenRoomExists()
        {
            // Arrange
            var room = new Room();
            _roomRepositoryMock.Setup(r => r.GetById(room.Id)).Returns(room);
            var roomRepository = _roomRepositoryMock.Object;
            var mapper = _mapperMock.Object;
            var sut = new RoomService(roomRepository, mapper);

            // Act
            sut.DeleteRoom(room.Id);

            // Assert
            _roomRepositoryMock.Verify(r => r.Delete(room), Times.Once);
        }

        [TestMethod]
        public void DeleteRoom_ShouldNotDeleteRoom_WhenRoomDoesNotExist()
        {
            // Arrange
            // Could just not mock it, but it's better to be explicit
            Room room = null;
            _roomRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(room);
            var roomRepository = _roomRepositoryMock.Object;
            var mapper = _mapperMock.Object;
            var sut = new RoomService(roomRepository, mapper);

            // Act
            sut.DeleteRoom(1);

            // Assert
            _roomRepositoryMock.Verify(r => r.Delete(It.IsAny<Room>()), Times.Never);
        }
    }
}