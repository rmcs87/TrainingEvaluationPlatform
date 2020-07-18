using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using TEP.Application.Common.Options;
using TEP.Infra.Files;
using TEP.Infra.Files.Exceptions;

namespace TEP.Infra.File.Tests
{
    [TestClass]
    public class FileServerTests
    {
        private readonly FileAssetOptions _options;
        private readonly Mock<ILogger<FileService>> _logger;
        private readonly string _imgAssetValidPath;
        private readonly string _invalidImgAssetValidPath;

        public FileServerTests()
        {
            _logger = new Mock<ILogger<FileService>>();

            _options = new FileAssetOptions();

            var sc = Path.DirectorySeparatorChar.ToString();

            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            _imgAssetValidPath = $"{baseTestProjectDirectory}TestFiles\\helmet.jpg";
            _imgAssetValidPath.Replace("\\", sc);
            _invalidImgAssetValidPath = $"{baseTestProjectDirectory}TestFiles\\gloves.jpg";
            _invalidImgAssetValidPath.Replace("\\", sc);
        }

        [TestMethod]
        public async Task OnCreateFile_WithValidData_CreatesAndReturnsPath()
        {
            //arrange
            var fileServer = new FileService(_logger.Object);
            fileServer.Options = _options;
            string imgName;
            FormFile file;

            using (var stream = System.IO.File.OpenRead(_imgAssetValidPath))
            {

                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/jpg"
                };

                //act
                imgName = await fileServer.SaveFile(file);

                //assert
                Assert.IsFalse(String.IsNullOrEmpty(imgName));
            }
        }

        [TestMethod]
        public void OnCreateFile_WithInvalidDataTooBig_ThrowsFileCreationException()
        {
            //arrange
            var fileServer = new FileService( _logger.Object);
            fileServer.Options = _options;
            FormFile file;

            using (var stream = System.IO.File.OpenRead(_invalidImgAssetValidPath))
            {

                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/jpg"
                };

                //act / assert            
                Assert.ThrowsExceptionAsync<FileCreationException>(() => fileServer.SaveFile(file));
            }
        }

        [TestMethod]
        public async Task OnRemoveFile_WithValidData_Removes()
        {
            //arrange
            var name = await CreateFile(_imgAssetValidPath);
            FileService fileServer = new FileService(_logger.Object);
            fileServer.Options = _options;

            //act
            fileServer.RemoveFile(name);

            //assert
            Assert.IsFalse(System.IO.File.Exists(name));
        }

        [TestMethod]
        public void OnCreateFile_WithInvalidPath_ThrowsFileCreationException()
        {
            //arrange
            FileService fileServer = new FileService(_logger.Object);
            fileServer.Options = _options;

            //act/assert
            Assert.ThrowsException<FileRemovalException>(() => fileServer.RemoveFile("/casa/nops.jpg"));
        }

        [TestMethod]
        public async Task OnGetFile_WithValidPath_Removes()
        {
            //arrange
            var fileServer = new FileService(_logger.Object);
            fileServer.Options = _options;
            FormFile file;
            string name;
            long size;
            using (var stream = System.IO.File.OpenRead(_imgAssetValidPath))
            {
                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/jpg"
                };
                size = file.Length;
                name = await fileServer.SaveFile(file);
            }

            //act
            byte[] fileBytes = await fileServer.GetFileBytes(name);

            //assert            
            Assert.AreEqual(size, fileBytes.Length);
        }

        [TestMethod]
        public void OnGetFile_WithInvalidPatha_ThrowsFileRetrievalException()
        {
            //arrange
            FileService fileServer = new FileService(_logger.Object);
            fileServer.Options = _options;

            //act/assert
            Assert.ThrowsExceptionAsync<FileRetrievalException>(() => fileServer.GetFileBytes("/casa/nops.jpg"));
        }


        private async Task<string> CreateFile(string path)
        {
            var fileServer = new FileService(_logger.Object);
            fileServer.Options = _options;
            FormFile file;
            string name;
            using (var stream = System.IO.File.OpenRead(path))
            {
                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/jpg"
                };
                name = await fileServer.SaveFile(file);
            }
            return name;
        }
    }
}
