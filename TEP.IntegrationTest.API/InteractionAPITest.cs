using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace TEP.IntegrationTest.API
{
    [TestClass]
    public class InteractionAPITest : Setup
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public InteractionAPITest()
        {
            var server = new ApiTestServer();
            _client = server.CreateClient();

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
        //[TestMethod]

    }
}
