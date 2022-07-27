## Integration tests
* In solution folder "99. Tests", add new project "MyHotel.IntegrationTests" by using the project template "MsTest Test Project" and the .Net 6 framework
* Right click project MyHotel.IntegrationTests, Manage NuGet Packages, find and search the NuGet packages
  * FluentAssertions
  * Microsoft.AspNetCore.Mvc.Testing
* Write the integration tests in a new class ReservationControllerTests for the ReservationController
  * Use the WebApplicationFactory in order to create the HttpClient, like in the article https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
* Check the test coverage of the application by using ReSharper, DotCover tool:
  * In Visual Studio, choose menu item "Extensions", select menu item "Unit tests", select "Cover all tests from solution"
