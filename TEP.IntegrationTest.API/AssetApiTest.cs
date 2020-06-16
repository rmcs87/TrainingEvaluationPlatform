using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using TEP.Servicos.Api;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using TEP.Domain.Entities;
using TEP.Appication.DTO;
using System;

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
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "api/asset");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public async Task OnRequestGetAssetById_WithValidId_ReceivesOk()
        {
            //Arrange
            int id = 1; //Database Dependent
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), $"api/asset/{id}");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnRequestGetAssetById_WithInalidId_ReceivesNotFound()
        {
            //Arrange
            int id = -1;
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), $"api/asset/{id}");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestInsertAsset_WithValidData_ReceivesOk()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_assetKeyValid);
            HttpRequestMessage requestMessage = PrepareHttpRequestMessage("POST", "api/asset", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }        

        [TestMethod]
        public async Task OnRequestInsertAsset_WithInvalidData_ReceivesBadRequest()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_assetKeyInvalid);
            HttpRequestMessage request = PrepareHttpRequestMessage("POST", "api/asset", json);

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnRequestUpdateAsset_WithValidData_ReceivesOk()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_assetKeyValid);
            HttpRequestMessage requestMessage = PrepareHttpRequestMessage("POST", "api/asset", json);
            
            var response = await _client.SendAsync(requestMessage);
            string responseId = await response.Content.ReadAsStringAsync();
            _assetKeyValid.Id = Convert.ToInt32(responseId);

            _assetKeyValid.Name = "updatedName";
            json = JsonSerializer.Serialize(_assetKeyValid);
            requestMessage = PrepareHttpRequestMessage("PUT", "api/asset", json);

            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnRequestUpdateAsset_WithInvalidData_ReceivesBadRequest()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_assetKeyValid);
            var requestMessage = PrepareHttpRequestMessage("POST", "api/asset", json);

            var response = await _client.SendAsync(requestMessage);
            string responseId = await response.Content.ReadAsStringAsync();
            _assetKeyInvalid.Id = Convert.ToInt32(responseId);

            _assetKeyValid.Name = "updatedName";
            json = JsonSerializer.Serialize(_assetKeyInvalid);
            requestMessage = PrepareHttpRequestMessage("PUT", "api/asset", json);

            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
