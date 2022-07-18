## Integration tests
* In solution folder "99. Tests", add new project "MyHotel.IntegrationTests" by using the project template "xUnit Test Project" and the .Net 6 framework
* Add new project  click project HumanitarianAid.API.IntegrationTests, Manage NuGet Packages, find and search the NuGet packages
  * FluentAssertions
  * Microsoft.AspNetCore.Mvc.Testing
* Write the integration test in the SheltersControllerTests for the ShelterController register shelter functionality
  * Use the WebApplicationFactory in order to create the HttpClient, like in the article https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
  * Use the POST method in the act section and the GET method in the Assert section 
