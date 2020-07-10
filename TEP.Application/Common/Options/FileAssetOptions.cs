using System;
using System.IO;
using TEP.Application.Common.Interfaces;

namespace TEP.Application.Common.Options
{
    public class FileAssetOptions : IFileOptions
    {
        public int SizeLimit => 64000;

        public string BasePath => GetPath();

        public string NameSalt => "AssetFile";

        public string[] SupportedFilesExtension => new string[] { ".jpg" };

        private string GetPath()
        {
            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            return $"{baseTestProjectDirectory}fileStorage";
        } 
    }
}
