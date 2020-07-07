//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using TEP.Application.Common.Options;
//using TEP.Infra.Files;
//using TEP.Infra.Files.Exceptions;

//namespace TEP.Infra.File.Tests
//{
//    [TestClass]
//    public class FileServerTests
//    {
//        private readonly FileAssetOptions _options;
//        private readonly Mock<ILogger> _logger;
//        private readonly string _imgAssetValidPath;
//        private readonly string _invalidImgAssetValidPath;
//        private readonly Mock<IOptionsMonitor<IFileOptions>> _optionProvider;

//        public FileServerTests()
//        {
//            _logger = new Mock<ILogger>();

//            _options = new FileAssetOptions();
//            _optionProvider = new Mock<IOptionsMonitor<IFileOptions>>();
//            _optionProvider.Setup(x => x.CurrentValue).Returns(_options);

//            var baseTestProjectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
//            _imgAssetValidPath = $"{baseTestProjectDirectory}TestFiles\\helmet.jpg";
//            _invalidImgAssetValidPath = $"{baseTestProjectDirectory}TestFiles\\gloves.jpg";
//        }

//        [TestMethod]
//        public async Task OnCreateFile_WithValidData_CreatesAndReturnsPath()
//        {
//            //arrange
//            var fileServer = new FileServer(_optionProvider.Object, _logger.Object);
//            string imgName;
//            FormFile file;

//            using (var stream = System.IO.File.OpenRead(_imgAssetValidPath))
//            {

//                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
//                {
//                    Headers = new HeaderDictionary(),
//                    ContentType = "application/jpg"
//                };

//                //act
//                imgName = await fileServer.SaveFile(file);

//                //assert
//                Assert.IsFalse(String.IsNullOrEmpty(imgName));
//            }
//        }

//        [TestMethod]
//        public void OnCreateFile_WithInvalidDataTooBig_ThrowsFileCreationException()
//        {
//            //arrange
//            var fileServer = new FileServer(_optionProvider.Object, _logger.Object);
//            FormFile file;

//            using (var stream = System.IO.File.OpenRead(_invalidImgAssetValidPath))
//            {

//                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
//                {
//                    Headers = new HeaderDictionary(),
//                    ContentType = "application/jpg"
//                };

//                //act / assert            
//                Assert.ThrowsExceptionAsync<FileCreationException>(() => fileServer.SaveFile(file));
//            }
//        }

//        [TestMethod]
//        public async Task OnRemoveFile_WithValidData_Removes()
//        {
//            //arrange
//            var name = await CreateFile(_imgAssetValidPath);
//            FileServer fileServer = new FileServer(_optionProvider.Object, _logger.Object);

//            //act
//            fileServer.RemoveFile(name);

//            //assert
//            var destinyPath = $"{_options.BasePath}\\{name}";
//            Assert.IsFalse(System.IO.File.Exists(name));
//        }

//        [TestMethod]
//        public void OnCreateFile_WithInvalidPath_ThrowsFileCreationException()
//        {
//            //arrange
//            FileServer fileServer = new FileServer(_optionProvider.Object, _logger.Object);

//            //act/assert
//            Assert.ThrowsException<FileRemovalException>(() => fileServer.RemoveFile("/casa/nops.jpg"));
//        }

//        [TestMethod]
//        public async Task OnGetFile_WithValidPath_Removes()
//        {
//            //arrange
//            var fileServer = new FileServer(_optionProvider.Object, _logger.Object);
//            FormFile file;
//            string name;
//            long size;
//            using (var stream = System.IO.File.OpenRead(_imgAssetValidPath))
//            {
//                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
//                {
//                    Headers = new HeaderDictionary(),
//                    ContentType = "application/jpg"
//                };
//                size = file.Length;
//                name = await fileServer.SaveFile(file);
//            }

//            //act
//            byte[] fileBytes = await fileServer.GetFileBytes(name);

//            //assert            
//            Assert.AreEqual(size, fileBytes.Length);
//        }

//        [TestMethod]
//        public void OnGetFile_WithInvalidPatha_ThrowsFileRetrievalException()
//        {
//            //arrange
//            FileServer fileServer = new FileServer(_optionProvider.Object, _logger.Object);

//            //act/assert
//            Assert.ThrowsExceptionAsync<FileRetrievalException>(() => fileServer.GetFileBytes("/casa/nops.jpg"));
//        }


//        private async Task<string> CreateFile(string path)
//        {
//            var fileServer = new FileServer(_optionProvider.Object, _logger.Object);
//            FormFile file;
//            string name;
//            using (var stream = System.IO.File.OpenRead(path))
//            {
//                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
//                {
//                    Headers = new HeaderDictionary(),
//                    ContentType = "application/jpg"
//                };
//                name = await fileServer.SaveFile(file);
//            }
//            return name;
//        }
//    }
//}
