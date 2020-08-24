using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;
using TEP.Application.Common.Options;
using TEP.Domain.Entities;
using TEP.Test.Utils;

namespace TEP.Application.Tests
{
    [TestClass]
    public class CreateAssetCommandsTests
    {
        private readonly Mock<IApplicationDbContext> _context;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IFileServiceFactory> _fileServiceFactory;
        private readonly Mock<IFileService> _fileService;
        private readonly CreateAssetCommand _createAssetCommand;
        private readonly List<Asset> _assetList;
        
        public CreateAssetCommandsTests()
        {
            _assetList = new List<Asset>();
            _createAssetCommand = new CreateAssetCommand { 
                FileURI = "webURI", Name = "moq", Image = new Mock<IFormFile>().Object,
                CategoriesIds = new List<int> { 2, 4 }
            };
            
            _context = new Mock<IApplicationDbContext>();            
            _mapper = new Mock<IMapper>();                        
            _fileService = new Mock<IFileService>();            
            _fileServiceFactory = new Mock<IFileServiceFactory>();            
        }        

        [TestMethod]
        public async Task OnHandleCreateAssetCommand_WithValidCommand_ReturnsAssetId()
        {
            //Arrange                
            _context.Setup(c => c.Assets).Returns(TestUtils.GetQueryableMockDbSet<Asset>(_assetList));
            _mapper.Setup(m => m.Map<Asset>(It.IsAny<CreateAssetCommand>())).Returns(new Asset());
            _fileService.Setup(f => f.SaveFile(It.IsAny<IFormFile>())).ReturnsAsync(new ServiceResponse<string> { Data = "iconPath" });
            _fileServiceFactory.Setup(fs => fs.Create<FileAssetOptions>()).Returns(_fileService.Object);

            var handler = new CreateAssetCommandHandler(_context.Object, _fileServiceFactory.Object, _mapper.Object);

            //Act
            var id = await handler.Handle(_createAssetCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.AreEqual(0,id);
        }
    }
}
