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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/asset");

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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/asset/{id}");

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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/asset/{id}");

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
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);

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
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);

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
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
            var response = await _client.SendAsync(requestMessage);
            string responseJson = await response.Content.ReadAsStringAsync();

            _newAssetKeyValid.Id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
            _newAssetKeyValid.Name = "updatedName";
            _newAssetKeyValid.ImgPath = "OldPath";

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
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
            var response = await _client.SendAsync(requestMessage);
            string responseJson = await response.Content.ReadAsStringAsync();

            _newAssetKeyValid.Id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
            _newAssetKeyValid.Name = "updatedName";
            _newAssetKeyValid.ImgPath = "OldPath";
            json = JsonSerializer.Serialize(_newAssetKeyValid);
            requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Put, "api/asset", json, _imgAssetValidPath2);
            
            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestUpdateAsset_WithInvalidData_ReceivesBadRequest()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);            
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
            var response = await _client.SendAsync(requestMessage);
            string responseJson = await response.Content.ReadAsStringAsync();

            _newAssetKeyValid.Id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
            _newAssetKeyValid.Name = "updatedName";
            _newAssetKeyValid.ImgPath = "OldPath";
            json = JsonSerializer.Serialize(_assetKeyInvalid);            
            requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Put, "api/asset", json, _imgAssetValidPath2);
            
            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    
        [TestMethod]
        public async Task OnRequestDeleteAssetById_WithValidId_ReceivesOk()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_newAssetKeyValid);            
            HttpRequestMessage requestMessage = PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
            var response = await _client.SendAsync(requestMessage);
            string responseJson = await response.Content.ReadAsStringAsync();


            var id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
            _newAssetKeyValid.Id = id;

            json = JsonSerializer.Serialize(_newAssetKeyValid);
            requestMessage = PrepareHttpRequestMessageAppJson(HttpMethod.Delete, $"api/asset/{id}", json);

            //Act
            response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestDeleteAssetById_WithInvalidId_ReceivesOk()
        {
            //Arrange
            var id = -1;

            var json = JsonSerializer.Serialize(_newAssetKeyValid);
            var requestMessage = PrepareHttpRequestMessageAppJson(HttpMethod.Delete, $"api/asset/{id}", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    
    }

    internal class Identifier
    {
        public int id { get; set; }
    }
}