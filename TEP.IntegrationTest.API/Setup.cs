using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Appication.Categories;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Assets.Commands.DeleteAsset;
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

        protected readonly DeleteAssetCommand _deletAssetKey;

        protected readonly string _imgAssetValidPath;
        protected readonly string _imgAssetValidPath2;
        protected readonly string _imgAssetValidPath3PNG;
        protected readonly string _imgAssetInvalidFileTXT;

        protected readonly ApplicationUser _validManagerUser;
        protected readonly ApplicationUser _validOperatorUser;
        protected readonly ApplicationUser _invalidUser;
        protected readonly ApplicationUser _erroFormatUser;

        public Setup()
        {
            _createAssetKeyValid = new CreateAssetCommand
            {
                Name = Guid.NewGuid().ToString(),
                FileURI = "key.fbx",
                CategoriesIds = new List<int> { 2, 4 }
            };
            _createAssetKeyInvalid = new CreateAssetCommand { Name = "", FileURI = "" };

            _updateAssetKeyValid = new UpdateAssetComamnd { 
                Name = Guid.NewGuid().ToString(), 
                FileURI = "key.fbx", 
                CategoriesIds = new List<int> { 2 } 
            };
            _updateAssetKeyInvalid = new UpdateAssetComamnd { Name = "", FileURI = "" };

            _deletAssetKey = new DeleteAssetCommand();

            _validManagerUser = new ApplicationUser { Username = "rico", Password = "r1c0" };
            _validOperatorUser = new ApplicationUser { Username = "joao", Password = "jonh" };
            _invalidUser = new ApplicationUser { Username = "rico", Password = "12345" };
            _erroFormatUser = new ApplicationUser { Username = "", Password = "12345" };

            var sc = Path.DirectorySeparatorChar.ToString();
            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            _imgAssetValidPath = $"{baseTestProjectDirectory}TestFiles\\helmet.jpg";
            _imgAssetValidPath.Replace("\\", sc);
            _imgAssetValidPath2 = $"{baseTestProjectDirectory}TestFiles\\gloves2.jpg";
            _imgAssetValidPath2.Replace("\\", sc);
            _imgAssetValidPath3PNG = $"{baseTestProjectDirectory}TestFiles\\helmet.png";
            _imgAssetValidPath3PNG.Replace("\\", sc);
            _imgAssetInvalidFileTXT = $"{baseTestProjectDirectory}TestFiles\\texto.txt";
            _imgAssetInvalidFileTXT.Replace("\\", sc);
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

        protected async Task<int> AddAsset(CreateAssetCommand createAssetKeyValid, HttpClient _client)
        {
            var stringContentsDictionary = ObjectAttributesToDicionary(createAssetKeyValid);
            HttpRequestMessage requestMessage =
                HttpRequestHelper.PrepareMultipartFormWithFile(HttpMethod.Post, "api/asset", stringContentsDictionary, _imgAssetValidPath);
            var response = await _client.SendAsync(requestMessage);

            string responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Identifier>(responseJson).id;
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
