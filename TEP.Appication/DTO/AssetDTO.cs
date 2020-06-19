using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEP.Appication.ModelBinders;

namespace TEP.Appication.DTO
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class AssetDTO : DTOBase
    {        
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public IFormFile Image { get; set; }
    }
}
