using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Assets.Commands.UpdateAsset;
using TEP.Application.Common.Models;
using TEP.Shared.Helpers;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly CreateAssetCommand _createAssetKeyValid;        
        protected readonly CreateAssetCommand _createAssetKeyInvalid;

        protected readonly UpdateAssetComamnd _updateAssetKeyValid;
        protected readonly UpdateAssetComamnd _updateAssetKeyInvalid;

        protected readonly string _imgAssetValidPath;
        protected readonly string _imgAssetValidPath2;

        protected readonly ApplicationUser _validManagerUser;
        protected readonly ApplicationUser _validOperatorUser;
        protected readonly ApplicationUser _invalidUser;
        protected readonly ApplicationUser _erroFormatUser;

        public Setup()
        {
            _createAssetKeyValid = new CreateAssetCommand { Name = Guid.NewGuid().ToString(), FilePath = "key.fbx" };
            _createAssetKeyInvalid = new CreateAssetCommand { Name = "", FilePath = "" };

            _updateAssetKeyValid = new UpdateAssetComamnd { Name = Guid.NewGuid().ToString(), FilePath = "key.fbx" };
            _updateAssetKeyInvalid = new UpdateAssetComamnd { Name = "", FilePath = "" };

            _validManagerUser = new ApplicationUser { Username = "rico", Password = "r1c0" };
            _validOperatorUser = new ApplicationUser { Username = "joao", Password = "jonh" };
            _invalidUser = new ApplicationUser { Username = "rico", Password = "12345" };
            _erroFormatUser = new ApplicationUser { Username = "", Password = "12345" };

            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            _imgAssetValidPath = $"{baseTestProjectDirectory}TestFiles\\helmet.jpg";
            _imgAssetValidPath2 = $"{baseTestProjectDirectory}\\TestFiles\\gloves2.jpg";
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

        protected Dictionary<string, string> ObjectAttributesToDicionary(object obj)
        {
            Dictionary<string, string> stringContents = new Dictionary<string, string>();
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();

            foreach (var prop in props)
            {
                stringContents.Add(prop.Name, JsonSerializer.Serialize(prop.GetValue(obj)));
            }

            return stringContents;
        }

    }
}
