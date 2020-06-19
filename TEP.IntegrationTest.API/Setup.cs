using System.IO;
using System.Net.Http;
using TEP.Appication.DTO;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly AssetDTO _newAssetKeyValid;
        protected readonly AssetDTO _updateAssetKeyValid;
        protected readonly AssetDTO _assetKeyInvalid;

        public Setup()
        {
            _newAssetKeyValid = new AssetDTO {Name = "key", FilePath = "key.fbx", ImgPath = "" };
            _assetKeyInvalid = new AssetDTO {Name = "", FilePath = "", ImgPath = "key.jpg" };
        }

        protected static HttpRequestMessage PrepareHttpRequestMessageAppJson(string method, string url, string json)
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
        //Files UpTo 64KB
        protected static HttpRequestMessage PrepareHttpRequestMessageMultipartFormDataWithSmallFile(HttpMethod method, string url, string json, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            byte[] fileContents = File.ReadAllBytes(fileInfo.FullName);

            var request = new HttpRequestMessage(method, url);
            request.Headers.ExpectContinue = false;


            MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyBoundary");

            ByteArrayContent byteArrayContent = new ByteArrayContent(fileContents);
            byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
            multiPartContent.Add(byteArrayContent, "Image", fileName);

            var jsonContent = new StringContent(
              json,
              System.Text.Encoding.UTF8,
              "application/json"
            );
            multiPartContent.Add(jsonContent, "json");

            request.Content = multiPartContent;

            return request;
        }
        
        protected static HttpRequestMessage PrepareHttpRequestMessageMultipartFormDataWithOutFile(HttpMethod method, string url, string json)
        {            
            var request = new HttpRequestMessage(method, url);
            request.Headers.ExpectContinue = false;


            MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyBoundary");

            var jsonContent = new StringContent(
              json,
              System.Text.Encoding.UTF8,
              "application/json"
            );
            multiPartContent.Add(jsonContent, "json");

            request.Content = multiPartContent;

            return request;
        }
    }
}
