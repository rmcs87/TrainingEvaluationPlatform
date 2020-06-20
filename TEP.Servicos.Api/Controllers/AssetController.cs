using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
                return BadRequest(ModelState);

            try
            {
                string fileName = FileHelper.GetNewUniqueName("AssetImage", data.Image.FileName);
                data.ImgPath = fileName;

                await FileHelper.ProcessAndValidateFile(
                    data.Image, new string[] { ".jpg", ".jpeg", ".png" }, _fileSizeLimit, _filePath, fileName);

                return new OkObjectResult(new { id = _app.Insert(data) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("{updatefile}")]
        public async Task<IActionResult> UpdateWithFile([FromBody] AssetDTO data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string fileName = FileHelper.GetNewUniqueName("AssetImage", data.Image.FileName);
                string oldFileName = data.ImgPath;
                data.ImgPath = fileName;

                await FileHelper.ProcessAndValidateFile(
                    data.Image, new string[] { ".jpg", ".jpeg", ".png" }, _fileSizeLimit, _filePath, fileName);

                System.IO.File.Delete(FileHelper.CombinePathAndName(_filePath, oldFileName));

                _app.Update(data);
                
                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public override IActionResult Delete(int id)
        {
            try
            {
                var assetDTO = _app.GetById(id);

                System.IO.File.Delete(FileHelper.CombinePathAndName(_filePath, assetDTO.ImgPath));

                _app.Delete(id);                

                return new OkObjectResult(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

