
namespace TEP.Domain.Entities.Assets
{
    public class SimpleAsset : EntityBase, IAsset
    {
        public SimpleAsset(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; private set; }
        public string Name { get; private set; }
    }
}
