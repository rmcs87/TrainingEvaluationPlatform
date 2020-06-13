
namespace TEP.Domain.Entities
{
    public class Asset : EntityBase
    {
        //Constructors
        private Asset()
        {

        }
        /// <summary>
        /// Constructor to create an Asset, which represents an physical object.
        /// </summary>
        /// <param name="filePath">Path to the file that represents the physical object.</param>
        /// <param name="name">The of this Asset.</param>
        /// <param name="imgPath">Path to the image that represents the physical object.</param>
        public Asset(string filePath, string name, string imgPath)
        {
            FilePath = filePath;
            Name = name;
            ImgPath = imgPath;
        }

        //Properties
        /// <summary>
        /// Gets the Path to the file.
        /// </summary>
        public string FilePath { get; private set; }
        /// <summary>
        /// Gets the name of this Asset.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the Path to the image.
        /// </summary>
        public string ImgPath { get; private set; }
    }
}
