using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using TEP.Shared.Helpers;
using System.Text.Json;
using TEP.Application.Assets.Queries.GetAsset;

namespace TEP.IntegrationTest.API
{
    [TestClass]
    public class AssetApiTest : Setup
    {
        private readonly HttpClient _client;

        JsonSerializerOptions _jsonOptions;

        public AssetApiTest()
        {
            var server = new ApiTestServer();
            _client = server.CreateClient();

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        [TestMethod]
        public async Task OnRequestAssetList_WithAuthorization_ReceivesOk()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/asset");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestAssetList_WithoutAuthorization_ReceivesForbiden()
        {
            //Arrange
            await AuthorizeClient(_client, _validOperatorUser);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/asset");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestGetAssetById_WithValidId_ReceivesOk()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);

            var id = await AddAsset(_createAssetKeyValid, _client);
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
            await AuthorizeClient(_client, _validManagerUser);

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
            await AuthorizeClient(_client, _validManagerUser);

            var stringContentsDictionary = ObjectAttributesToDicionary(_createAssetKeyValid);
            HttpRequestMessage requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Post, "api/asset", stringContentsDictionary, _imgAssetValidPath3PNG);

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
            await AuthorizeClient(_client, _validManagerUser); 

            var stringContentsDictionary = ObjectAttributesToDicionary(_createAssetKeyInvalid);
            HttpRequestMessage requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Post, "api/asset", stringContentsDictionary, _imgAssetValidPath);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestInsertAsset_WithValidDataAndInvalidFile_ReceivesBadRequest()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);

            var stringContentsDictionary = ObjectAttributesToDicionary(_createAssetKeyValid);
            HttpRequestMessage requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Post, "api/asset", stringContentsDictionary, _imgAssetInvalidFileTXT);

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
            await AuthorizeClient(_client, _validManagerUser);

            _updateAssetKeyValid.Id = await AddAsset(_createAssetKeyValid, _client);
            _updateAssetKeyValid.Name += "updatedName";

            var stringContentsDictionary = ObjectAttributesToDicionary(_updateAssetKeyValid);
            var requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Put, "api/asset", stringContentsDictionary);            

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestUpdateFileAsset_WithValidData_ReceivesOk()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);

            _updateAssetKeyValid.Id = await AddAsset(_createAssetKeyValid, _client);
            _updateAssetKeyValid.Name += "updatedName";

            var stringContentsDictionary = ObjectAttributesToDicionary(_updateAssetKeyValid);
            var requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Put, "api/asset", stringContentsDictionary, _imgAssetValidPath2);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestUpdateAsset_WithInvalidData_ReceivesBadRequest()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);
            _updateAssetKeyInvalid.Id = await AddAsset(_createAssetKeyValid, _client);

            var stringContentsDictionary = ObjectAttributesToDicionary(_updateAssetKeyInvalid);
            var requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Put, "api/asset", stringContentsDictionary, _imgAssetValidPath2);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnRequestDeleteAssetById_WithValidId_ReceivesOk()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);           
            var id = await AddAsset(_createAssetKeyValid, _client);
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"api/asset/{id}");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }        

        [TestMethod]
        public async Task OnRequestDeleteAssetById_WithInvalidId_ReceivesBadRequest()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);
            var id = -1;

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"api/asset/{id}");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestAssetImage_WithValidURL_ReceivesImg()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);
            var id = await AddAsset(_createAssetKeyValid, _client);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/asset/{id}");
            var response = await _client.SendAsync(requestMessage);
            string responseJson = await response.Content.ReadAsStringAsync();

            string url = JsonSerializer.Deserialize<AssetDTO>(responseJson, _jsonOptions).IconPath;

            //Act
            requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnRequestAssetImage_WithInvalidURL_ReceivesBadRequest()
        {
            //Arrange
            await AuthorizeClient(_client, _validManagerUser);

            //Act
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/asset/image/nops");
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    internal class Identifier
    {
        public int id { get; set; }
    }
    /*
    internal class Asset
    {
        public int id { get; set; }
        public string filePath { get; set; }
        public string name { get; set; }
        public string imgPath { get; set; }
    }
    */
}
