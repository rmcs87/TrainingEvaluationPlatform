using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.DTO;
using TEP.Application.Interfaces;
using TEP.Domain.Entities;
using TEP.Presentation.Api.Controllers.Authorizers;
using TEP.Shared;
using TEP.Shared.Helpers;

namespace TEP.Presentation.Api.Controllers
{
    public class AssetController : ControllerBase<Asset, AssetDTO>
    {
        private readonly long _fileSizeLimit;
        private readonly string _filePath;
        private readonly string[] _supportedFilesExtension;
        private readonly string _fileNameSalt;

        public AssetController(IAppBase<Asset, AssetDTO> app, IConfiguration config) : base(app)
        {
            _fileSizeLimit = config.GetValue<long>("AssetImageSizeLimit");
            _filePath = config.GetValue<string>("AssetImageFilesPath");
            _supportedFilesExtension = new string[] { ".jpg", ".jpeg", ".png" };
            _fileNameSalt = "AssetImage";
        }

        [HttpPost]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public override async Task<IActionResult> Insert([FromBody] AssetDTO data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await SaveAssetImageInDisk(data);
                return new OkObjectResult(new { id = _app.Insert(data) });
            }
            catch (IOException fileCreationError)
            {
                return BadRequest(fileCreationError.Message);
            }
            catch (Exception insertEntityError)
            {
                FileHelper.DeleteFromDisk(FileHelper.CombinePathAndName(_filePath, data.Image.FileName));
                return BadRequest(insertEntityError.Message);
            }
        }

        /// <summary>
        /// Updates Entity Asset. Must resceive a MultipartFormData.
        /// </summary>
        /// <param name="data">Asset Model to be updated, with or without file</param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public override async Task<IActionResult> Update([FromBody] AssetDTO data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Update changing image file
                if(data.Image != null)
                {
                    string oldFileName = data.ImgPath;
                    await SaveAssetImageInDisk(data);
                    FileHelper.DeleteFromDisk(FileHelper.CombinePathAndName(_filePath, oldFileName));
                }

                _app.Update(data);
                
                return new OkObjectResult(true);
            }
            catch (IOException fileCreationError)
            {                
                return BadRequest(fileCreationError.Message);
            }
            catch (Exception updateEntityError)
            {
                FileHelper.DeleteFromDisk(FileHelper.CombinePathAndName(_filePath, data.Image.FileName));
                return BadRequest(updateEntityError.Message);
            }
        }        

        [HttpDelete]
        [Route("{id}")]
        [AuthorizePolicy(UserPolicies.ManagerRights)]
        public override IActionResult Delete(int id)
        {
            try
            {
                var assetDTO = _app.GetById(id);
                FileHelper.DeleteFromDisk(FileHelper.CombinePathAndName(_filePath, assetDTO.ImgPath));
                _app.Delete(id);                

                return new OkObjectResult(new { deleted = true });
            }
            catch (IOException deleteFileError)
            {
                return BadRequest(deleteFileError.Message);
            }
            catch (Exception deleteEntityError)
            {
                var message = deleteEntityError.Message + " Corresponding Image may have been corrupted.";
                return BadRequest(message);
            }
        }

        private async Task SaveAssetImageInDisk(AssetDTO data)
        {
            try
            {
                string fileName = FileHelper.GetNewUniqueName(_fileNameSalt, data.Image.FileName);
                data.ImgPath = fileName;

                await FileHelper.ProcessAndValidateFile(
                    data.Image, _supportedFilesExtension, _fileSizeLimit, _filePath, fileName);
            }
            catch (IOException ioe)
            {
                throw ioe;
            }            
        }
    }
}

