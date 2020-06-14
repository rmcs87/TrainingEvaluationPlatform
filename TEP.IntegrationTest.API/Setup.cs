using TEP.Appication.DTO;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly AssetDTO _assetKeyValid;
        protected readonly AssetDTO _assetKeyInvalid;

        public Setup()
        {
            _assetKeyValid = new AssetDTO {Name = "key", FilePath = "key.fbx", ImgPath = "key.jpg" };
            _assetKeyInvalid = new AssetDTO {Name = "", FilePath = "", ImgPath = "key.jpg" };
        }
    }
}
