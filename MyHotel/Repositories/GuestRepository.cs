using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using MyHotel.Entities;

namespace MyHotel.Persistance.Repositories
{
    public class GuestRepository : BaseRepository<Guest>, IGuestRepository
    {
        public GuestRepository(MyHotelDbContext myHotelDbContext): base(myHotelDbContext)
        {

        }
    }
}
