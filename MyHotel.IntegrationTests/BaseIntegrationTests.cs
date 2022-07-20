using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHotel.Api.Controllers;
using MyHotel.Persistance.Data;

namespace MyHotel.IntegrationTests
{
    public class BaseIntegrationTests
    {
        private WebApplicationFactory<ReservationController> _application;

        protected HttpClient HttpClient { get; private set; }

        [TestInitialize]
        public async Task TestInitialize()
        {
            _application = new WebApplicationFactory<ReservationController>()
                .WithWebHostBuilder(_ => { });

            HttpClient = _application.CreateClient();

            await CleanupDatabase();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await CleanupDatabase();
        }

        private async Task CleanupDatabase()
        {
            using var scope = _application.Services.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<MyHotelDbContext>();
            databaseContext.Database.Migrate();
            databaseContext.Reservations.RemoveRange(databaseContext.Reservations.ToList());
            databaseContext.Guests.RemoveRange(databaseContext.Guests.ToList());
            databaseContext.Rooms.RemoveRange(databaseContext.Rooms.ToList());
            await databaseContext.SaveChangesAsync();
        }
    }
}
