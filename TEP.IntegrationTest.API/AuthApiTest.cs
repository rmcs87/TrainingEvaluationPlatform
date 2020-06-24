﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Servicos.Api;
using TEP.Shared.Helpers;

namespace TEP.IntegrationTest.API
{
    [TestClass]
    public class AuthApiTest : Setup
    {
        private readonly HttpClient _client;
        public AuthApiTest()
        {            
            var server = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>()
            );
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task OnLogin_WithValidCredential_ReturnsToken()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_validUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/login", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task OnLogin_WithInvalidCredential_ReturnsNotFoud()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_invalidUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/login", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        } 
        

        [TestMethod]
        public async Task OnAccessRestrictedEndPoint_WithoutToken_ReturnsNotAuthorized()
        {
            //Arrange
            var json = JsonSerializer.Serialize(_invalidUser);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Get, "api/login/auth_test", json);

            //Act
            var response = await _client.SendAsync(requestMessage);

            //Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}