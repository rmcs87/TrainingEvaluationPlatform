using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Application.Common.Models;
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

        protected readonly ApplicationUser _validManagerUser;
        protected readonly ApplicationUser _validOperatorUser;
        protected readonly ApplicationUser _invalidUser;

        public Setup()
        {
            _newAssetKeyValid = new AssetDTO { Name = "key", FilePath = "key.fbx", ImgPath = "" };
            _assetKeyInvalid = new AssetDTO { Name = "", FilePath = "", ImgPath = "key.jpg" };

            _validManagerUser = new ApplicationUser { Username = "rico", Password = "r1c0" };
            _validOperatorUser = new ApplicationUser { Username = "joao", Password = "jonh" };
            _invalidUser = new ApplicationUser { Username = "rico", Password = "12345" };

            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            _imgAssetValidPath = $"{baseTestProjectDirectory}\\TestFiles\\helmet.jpg";
            _imgAssetValidPath2 = $"{baseTestProjectDirectory}\\TestFiles\\smallHelmet.png";
        }

        protected async Task AuthorizeClient(HttpClient client, ApplicationUser user)
        {
            var json = JsonSerializer.Serialize(user);
            HttpRequestMessage requestMessage = HttpRequestHelper.PrepareHttpRequestMessageAppJson(HttpMethod.Post, "api/login", json);
            var response = await client.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            var propertyName = "accessToken";
            var token = JsonSerializer.Deserialize<JsonElement>(responseContent)
                .GetProperty(propertyName)
                .GetString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
