using TEP.Appication.DTO;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly AssetDTO _assetKey;

        public Setup()
        {
            _assetKey = new AssetDTO {Name = "key", FilePath = "key.fbx", ImgPath = "key.jpg" };
        }
    }
}
