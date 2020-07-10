using System;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.Files
{
    internal class DefaultOptions : IFileOptions
    {
        public int SizeLimit => 1000000;

        public string BasePath => AppContext.BaseDirectory;

        public string NameSalt => "DEFAULT_OPTION";

        public string[] SupportedFilesExtension => new string[] {"jpg"};
    }
}