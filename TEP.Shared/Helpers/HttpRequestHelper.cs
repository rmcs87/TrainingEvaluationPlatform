using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace TEP.Shared.Helpers
{
    public static class HttpRequestHelper
    {
        //Files UpTo 64KB
        public static HttpRequestMessage PrepareMultipartFormWithFile(
            HttpMethod method, string url, Dictionary<string, string> stringContents, string filePath = null)
        {
            MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyBoundary");

            foreach (var content in stringContents)
            {
                multiPartContent.Add(CreateJsonContent(content.Value), content.Key);
            }


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

    }
}
