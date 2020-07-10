namespace TEP.Application.Common.Interfaces
{
    public interface IFileOptions
    {
        public int SizeLimit { get; }
        public string BasePath { get; }
        public string NameSalt { get; }
        public string [] SupportedFilesExtension { get; }
    }
}
