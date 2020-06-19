using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Shared.Helpers;

namespace TEP.Servicos.Api.Controllers
{
    public class AssetController : ControllerBase<Asset, AssetDTO>
    {
        private readonly long _fileSizeLimit;
        private readonly string _filePath;

        public AssetController(IAppBase<Asset, AssetDTO> app, IConfiguration config) : base(app)
        {
            _fileSizeLimit = config.GetValue<long>("AssetImageSizeLimit");
            _filePath = config.GetValue<string>("AssetImageFilesPath");
        }

        [HttpPost]
        public override async Task<IActionResult> Insert([FromBody] AssetDTO data)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetModelStateErrosAsJson());

            try
            {
                string fileName = FileHelper.GetNewUniqueName("AssetImage", data.Image.FileName);
                data.ImgPath = fileName;

                await FileHelper.ProcessAndValidateFile(
                    data.Image, new string[] { ".jpg", ".jpeg", "png" }, _fileSizeLimit, _filePath, fileName);

                return new OkObjectResult(new { id = _app.Insert(data) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
    }
}

