using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using TEP.Servicos.Api;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
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
            var responseContent = await response.Content.ReadAsStringAsync();
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
            var responseContent = await response.Content.ReadAsStringAsync();
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
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }      

        [TestMethod]
        public async Task OnRequestInsertAsset_WithValidData_ReceivesOk()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            var imgPath = @"C:\Users\rmcs8\source\repos\TrainingEvaluationPlatform\TEP.IntegrationTest.API\TestFiles\helmet.jpg";
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, imgPath);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }        

        [TestMethod]
        public async Task OnRequestInsertAsset_WithInvalidData_ReceivesBadRequest()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_assetKeyInvalid);
            var imgPath = @"C:\Users\rmcs8\source\repos\TrainingEvaluationPlatform\TEP.IntegrationTest.API\TestFiles\helmet.jpg";
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, imgPath);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnRequestUpdateInfoOnlyAsset_WithValidData_ReceivesOk()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            var imgPath = @"C:\Users\rmcs8\source\repos\TrainingEvaluationPlatform\TEP.IntegrationTest.API\TestFiles\helmet.jpg";
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, imgPath);

            var response = await _client.SendAsync(requestMessage);
            string responseJson = await response.Content.ReadAsStringAsync();

            Identificador idObject = JsonSerializer.Deserialize<Identificador>(responseJson);

            _newAssetKeyValid.Id = idObject.id;
            _newAssetKeyValid.Name = "updatedName";
            _newAssetKeyValid.ImgPath = "Should be the Path original";
            json = JsonSerializer.Serialize(_newAssetKeyValid);
            requestMessage = PrepareHttpRequestMessageMultipartFormDataWithOutFile(HttpMethod.Put, "api/asset", json);

            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestUpdateFileAsset_WithValidData_ReceivesOk()
        {

        }

        [TestMethod]
        public async Task OnRequestUpdateAsset_WithInvalidData_ReceivesBadRequest()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            var imgPath = @"C:\Users\rmcs8\source\repos\TrainingEvaluationPlatform\TEP.IntegrationTest.API\TestFiles\helmet.jpg";
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, imgPath);

            var response = await _client.SendAsync(requestMessage);
            string responseId = await response.Content.ReadAsStringAsync();
            _assetKeyInvalid.Id = Convert.ToInt32(responseId);

            _newAssetKeyValid.Name = "updatedName";
            json = JsonSerializer.Serialize(_assetKeyInvalid);
            requestMessage = PrepareHttpRequestMessageAppJson("PUT", "api/asset", json);

            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    
        [TestMethod]
        public async Task OnRequestDeleteAssetById_WithValidId_ReceivesOk()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            var imgPath = @"C:\Users\rmcs8\source\repos\TrainingEvaluationPlatform\TEP.IntegrationTest.API\TestFiles\helmet.jpg";
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, imgPath);

            var response = await _client.SendAsync(requestMessage);
            string responseId = await response.Content.ReadAsStringAsync();
            var id = Convert.ToInt32(responseId);

            json = JsonSerializer.Serialize(_assetKeyInvalid);
            requestMessage = PrepareHttpRequestMessageAppJson("DELETE", $"api/asset/{id}", json);

            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestDeleteAssetById_WithInvalidId_ReceivesOk()
        {
            //Arrange
            var id = -1;

            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            var requestMessage = PrepareHttpRequestMessageAppJson("DELETE", $"api/asset/{id}", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    
    }

    internal class Identificador
    {
        public int id { get; set; }
    }
}
