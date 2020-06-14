using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using TEP.Servicos.Api;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

namespace TEP.IntegrationTest.API
{    
    [TestClass]
    public class AssetApiTest : Setup
    {
        private readonly HttpClient _client;

        public AssetApiTest()
        {
            var server = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>()
            );
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task OnRequestAssetList_ReceivesOk()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "api/asset");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public async Task OnRequestGetAssetById_WithValidId_ReceivesOk()
        {
            //Arrange
            int id = 1; //Database Dependent
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/asset/{id}");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnRequestGetAssetById_WithInalidId_ReceivesNotFound()
        {
            //Arrange
            int id = -1;
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/asset/{id}");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestInsertAsset_WithValidData_ReceivesOk()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/asset");
            var json = JsonSerializer.Serialize(_assetKeyValid);
            var content = new StringContent(
              json,
              System.Text.Encoding.UTF8,
              "application/json"
            );
            request.Content = content;
            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        } 
        
        [TestMethod]
        public async Task OnRequestInsertAsset_WithInvalidData_ReceivesbadRequest()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/asset");
            var json = JsonSerializer.Serialize(_assetKeyInvalid);
            var content = new StringContent(
              json,
              System.Text.Encoding.UTF8,
              "application/json"
            );
            request.Content = content;
            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
