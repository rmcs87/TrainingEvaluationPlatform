using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Services.AuthProvider.Models;
using TEP.Shared.Helpers;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly AssetDTO _newAssetKeyValid;
        protected readonly AssetDTO _updateAssetKeyValid;
        protected readonly AssetDTO _assetKeyInvalid;
        protected readonly string _imgAssetValidPath;
        protected readonly string _imgAssetValidPath2;

        protected readonly User _validUser;
        protected readonly User _invalidUser;

        public Setup()
        {
            _newAssetKeyValid = new AssetDTO { Name = "key", FilePath = "key.fbx", ImgPath = "" };
            _assetKeyInvalid = new AssetDTO { Name = "", FilePath = "", ImgPath = "key.jpg" };

            _validUser = new User { Username = "rico", Password = "r1c0" };
            _invalidUser = new User { Username = "rico", Password = "12345" };

            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            _imgAssetValidPath = $"{baseTestProjectDirectory}\\TestFiles\\helmet.jpg";
            _imgAssetValidPath2 = $"{baseTestProjectDirectory}\\TestFiles\\smallHelmet.png";
        }

        protected async Task<string> GetAccessTokenAsync(HttpClient client, User user)
        {
            var json = JsonSerializer.Serialize(user);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/login", json);
            var response = await client.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            var propertyName = "token";
            var token = JsonSerializer.Deserialize<JsonElement>(responseContent)
                .GetProperty(propertyName)
                .GetString();
            return token;
        }

    }
}
