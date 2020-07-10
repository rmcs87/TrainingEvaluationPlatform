
namespace TEP.Application.Common.Interfaces
{
    public interface IFileServiceFactory
    {
        IFileService Create(FileProfile profile);
    }

    public enum FileProfile{
        AssetImage
    }
}
