using System.Net.Http;
using TEP.Appication.DTO;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly AssetDTO _assetKeyValid;
        protected readonly AssetDTO _assetKeyInvalid;

        public Setup()
        {
            _assetKeyValid = new AssetDTO {Name = "key", FilePath = "key.fbx", ImgPath = "key.jpg" };
            _assetKeyInvalid = new AssetDTO {Name = "", FilePath = "", ImgPath = "key.jpg" };
        }

        protected static HttpRequestMessage PrepareHttpRequestMessage(string method, string url, string json)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), url);
            var content = new StringContent(
              json,
              System.Text.Encoding.UTF8,
              "application/json"
            );
            request.Content = content;
            return request;
        }
    }
}
