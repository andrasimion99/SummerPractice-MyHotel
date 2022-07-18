# SummerPractice-MyHotel

## Migrate .Net 5 to .Net 6, [the last .Net LTS version provided by Microsoft](https://dotnet.microsoft.com/en-us/platform/support/policy)
* Go to all project files "*csproj" by double click on the project and change the TargetFramework from net5.0 into net6.0
* Right click solution and select "Manage Nuget Packages for Solution..."
  * Go to Updates menu and choose "Select all packages"
  * Press "Update"
* Go to ExceptionHandlerMiddleware class and remove "using Newtonsoft.Json;", since .Net 6 does not use the library Newtonsoft.Json for JSON serialization
  * Replace "JsonConvert.SerializeObject" with "JsonSerializer.Serialize"
  * Include the new "using System.Text.Json" for the JSON library used in .Net 6
