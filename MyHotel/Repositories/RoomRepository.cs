using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using MyHotel.Entities;

namespace MyHotel.Persistance.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(MyHotelDbContext myHotelDbContext) : base(myHotelDbContext)
        {

        }
    }
}
