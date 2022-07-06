using MyHotel.Data;
using MyHotel.Domain.IRepositories;
using MyHotel.Entities;

namespace MyHotel.Persistance.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRespository
    {
        public ReservationRepository(MyHotelDbContext myHotelDbContext): base(myHotelDbContext)
        {

        }

        public bool CheckAvailability(Reservation reservation)
        {

        }
    }
}
