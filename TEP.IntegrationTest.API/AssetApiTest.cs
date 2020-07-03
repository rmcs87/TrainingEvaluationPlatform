//using Microsoft.AspNetCore.TestHost;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Net;
//using System.Text.Json;
//using TEP.Shared.Helpers;
//using TEP.Presentation.Api;

//namespace TEP.IntegrationTest.API
//{
//    [TestClass]
//    public class AssetApiTest : Setup
//    {
//        private readonly HttpClient _client;

//        public AssetApiTest()
//        {
//            var server = new TestServer(
//                new WebHostBuilder()
//                    .UseEnvironment("Development")
//                    .UseStartup<Startup>()
//            );
//            _client = server.CreateClient();
//        }

//        [TestMethod]
//        public async Task OnRequestAssetList_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);
//            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/asset");

//            //Act
//            var response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }
       
//        [TestMethod]
//        public async Task OnRequestGetAssetById_WithValidId_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            int id = 1; //Database Dependent
//            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/asset/{id}");

//            //Act
//            var response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }
        
//        [TestMethod]
//        public async Task OnRequestGetAssetById_WithInalidId_ReceivesNotFound()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            int id = -1;
//            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/asset/{id}");

//            //Act
//            var response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
//        }      

//        [TestMethod]
//        public async Task OnRequestInsertAsset_WithValidData_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            var json = JsonSerializer.Serialize(_newAssetKeyValid);
//            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);

//            //Act
//            var response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }        

//        [TestMethod]
//        public async Task OnRequestInsertAsset_WithInvalidData_ReceivesBadRequest()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            var json = JsonSerializer.Serialize(_assetKeyInvalid);
//            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);

//            //Act
//            var response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
//        }
        
//        [TestMethod]
//        public async Task OnRequestUpdateInfoOnlyAsset_WithValidData_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            var json = JsonSerializer.Serialize(_newAssetKeyValid);
//            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
//            var response = await _client.SendAsync(requestMessage);
//            string responseJson = await response.Content.ReadAsStringAsync();

//            _newAssetKeyValid.Id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
//            _newAssetKeyValid.Name = "updatedName";
//            _newAssetKeyValid.ImgPath = "OldPath";

//            json = JsonSerializer.Serialize(_newAssetKeyValid);
//            requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Put, "api/asset", json);

//            //Act
//            response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }

//        [TestMethod]
//        public async Task OnRequestUpdateFileAsset_WithValidData_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            var json = JsonSerializer.Serialize(_newAssetKeyValid);
//            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
//            var response = await _client.SendAsync(requestMessage);
//            string responseJson = await response.Content.ReadAsStringAsync();

//            _newAssetKeyValid.Id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
//            _newAssetKeyValid.Name = "updatedName";
//            _newAssetKeyValid.ImgPath = "OldPath";
//            json = JsonSerializer.Serialize(_newAssetKeyValid);
//            requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Put, "api/asset", json, _imgAssetValidPath2);
            
//            //Act
//            response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }

//        [TestMethod]
//        public async Task OnRequestUpdateAsset_WithInvalidData_ReceivesBadRequest()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            var json = JsonSerializer.Serialize(_newAssetKeyValid);            
//            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
//            var response = await _client.SendAsync(requestMessage);
//            string responseJson = await response.Content.ReadAsStringAsync();

//            _newAssetKeyValid.Id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
//            _newAssetKeyValid.Name = "updatedName";
//            _newAssetKeyValid.ImgPath = "OldPath";
//            json = JsonSerializer.Serialize(_assetKeyInvalid);            
//            requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Put, "api/asset", json, _imgAssetValidPath2);
            
//            //Act
//            response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
//        }
    
//        [TestMethod]
//        public async Task OnRequestDeleteAssetById_WithValidId_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);

//            var json = JsonSerializer.Serialize(_newAssetKeyValid);            
//            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageMultipartFormDataJsonAndFile(HttpMethod.Post, "api/asset", json, _imgAssetValidPath);
//            var response = await _client.SendAsync(requestMessage);
//            string responseJson = await response.Content.ReadAsStringAsync();


//            var id = JsonSerializer.Deserialize<Identifier>(responseJson).id;
//            _newAssetKeyValid.Id = id;

//            json = JsonSerializer.Serialize(_newAssetKeyValid);
//            requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Delete, $"api/asset/{id}", json);

//            //Act
//            response = await _client.SendAsync(requestMessage);

//            //Assert
//            var responseContent = await response.Content.ReadAsStringAsync();
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        }

//        [TestMethod]
//        public async Task OnRequestDeleteAssetById_WithInvalidId_ReceivesOk()
//        {
//            //Arrange
//            await AuthorizeClient(_client, _validManagerUser);
//            var id = -1;

//            var json = JsonSerializer.Serialize(_newAssetKeyValid);
//            var requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Delete, $"api/asset/{id}", json);

//            //Act
//            var response = await _client.SendAsync(requestMessage);

//            //Assert
//            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
//        }
    
//    }

//    internal class Identifier
//    {
//        public int id { get; set; }
//    }
//}
