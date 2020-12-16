using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TEP.Presentation.Api;
using Microsoft.Extensions.Configuration;

public class ApiTestServer : TestServer
{
    public ApiTestServer() : base(WebHostBuilder())
    { }

    private static IWebHostBuilder WebHostBuilder() =>
      WebHost.CreateDefaultBuilder()
        .UseStartup<Startup>()
        .UseEnvironment("Development")
        .UseSetting("ConnectionStrings:teps", "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tep;Integrated Security=True;MultipleActiveResultSets=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        .ConfigureAppConfiguration(config => config.AddUserSecrets<Startup>());
}