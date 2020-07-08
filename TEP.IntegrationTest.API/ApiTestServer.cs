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
        .ConfigureAppConfiguration(config => config.AddUserSecrets<Startup>());
}