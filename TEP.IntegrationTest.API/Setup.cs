using System;
using System.IO;
using TEP.Appication.DTO;
using TEP.Services.AuthProvider.Models;

namespace TEP.IntegrationTest.API
{
    public class Setup
    {
        protected readonly AssetDTO _newAssetKeyValid;
        protected readonly AssetDTO _updateAssetKeyValid;
        protected readonly AssetDTO _assetKeyInvalid;
        protected readonly string _imgAssetValidPath;
        protected readonly string _imgAssetValidPath2;

        protected readonly User _validUser;
        protected readonly User _invalidUser;

        public Setup()
        {
            _newAssetKeyValid = new AssetDTO {Name = "key", FilePath = "key.fbx", ImgPath = "" };
            _assetKeyInvalid = new AssetDTO {Name = "", FilePath = "", ImgPath = "key.jpg" };

            _validUser = new User{Username = "robin", Password = "robin"};
            _invalidUser = new User{Username = "robin", Password = "hood"};

            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            _imgAssetValidPath = $"{baseTestProjectDirectory}\\TestFiles\\helmet.jpg";
            _imgAssetValidPath2 = $"{baseTestProjectDirectory}\\TestFiles\\smallHelmet.png";            
        }            
    }
}
