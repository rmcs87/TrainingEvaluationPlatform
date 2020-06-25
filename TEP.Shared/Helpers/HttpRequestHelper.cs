using System.IO;
using System.Net.Http;

namespace TEP.Shared.Helpers
{
    public static class HttpRequestHelper
    {
        private static StringContent CreateJsonContent(string json)
        {
            return new StringContent(
                          json,
                          System.Text.Encoding.UTF8,
                          "application/json"
                        );
        }

        public static HttpRequestMessage PrepareHttpRequestMessageAppJson(HttpMethod method, string url, string json)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = CreateJsonContent(json)
            };

            return request;
        }

        private static void CreateFileContent(string filePath, out string fileName, out ByteArrayContent byteArrayContent)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            fileName = fileInfo.Name;
            byte[] fileContents = File.ReadAllBytes(fileInfo.FullName);

            byteArrayContent = new ByteArrayContent(fileContents);
            byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
        }

        //Files UpTo 64KB
        public static HttpRequestMessage PrepareHttpRequestMessageMultipartFormDataJsonAndFile(
            HttpMethod method, string url, string json, string filePath = null)
        {
            MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyBoundary");

            multiPartContent.Add(CreateJsonContent(json), "json");

            if (filePath != null)
            {
                ByteArrayContent byteArrayContent;
                CreateFileContent(filePath, out string fileName, out byteArrayContent);

                multiPartContent.Add(byteArrayContent, "Image", fileName);
            }

            var request = new HttpRequestMessage(method, url);
            request.Headers.ExpectContinue = false;
            request.Content = multiPartContent;

            return request;
        }   
    }
}
