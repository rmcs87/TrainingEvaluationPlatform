using System;

namespace TEP.Application.Common.Options
{
    public class FileAssetOptions : IFileOptions
    {
        public int SizeLimit => 64000;

        public string BasePath => "C:\\Users\\rmcs8\\source\\repos\\TrainingEvaluationPlatform\\fileStorage";

        public string NameSalt => "AssetFile";

        public string[] SupportedFilesExtension => new string[] { ".jpg", ".jpeg", ".png" };
    }
}
