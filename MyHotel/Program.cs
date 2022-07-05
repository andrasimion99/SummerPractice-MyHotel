using MyHotel.Data;
using MyHotel.Entities;

namespace MyHotel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();
            var guest = new Guest()
            {
                FirstName = "Georgiana",
                LastName = "Botez",
                Email = "georgiana.botez@centric.eu"
            };

            context.Guests.Add(guest);
            context.SaveChanges();
        }
    }
}
