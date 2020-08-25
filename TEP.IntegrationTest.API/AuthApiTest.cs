using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Shared.Helpers;

namespace TEP.IntegrationTest.API
{
    [TestClass]
    public class AuthApiTest : Setup
    {
        private readonly HttpClient _client;
   
        public AuthApiTest()
        {
            var server = new ApiTestServer();
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task OnLogin_WithValidCredential_ReturnsToken()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_validManagerUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/v1/login", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task OnLogin_WithEmptyUsername_ReturnsBadRequest()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_erroFormatUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/v1/login", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task OnLogin_WithInvalidCredential_ReturnsNotFoud()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_invalidUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/v1/login", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task OnAccessRestrictedEndPoint_WithoutToken_ReturnsNotAuthorized()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_invalidUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Get, "api/v1/login/auth_test", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task OnAccessRestrictedEndPoint_WithTokenAndRights_ReturnsOk()
        {
            //Arrange   
            await AuthorizeClient(_client, _validManagerUser);

            var requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Get, "api/v1/login/auth_test", "");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnAccessRestrictedEndPoint_WithTokenButNoRights_ReturnsForbidden()
        {
            //Arrange   
            await AuthorizeClient(_client, _validOperatorUser);
            var requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Get, "api/v1/login/auth_test", "");

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }

}
