using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.PipelineBehaviours;

namespace TEP.Application.Tests.Common.Behaviors
{
    [TestClass]
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreateAssetCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;

        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateAssetCommand>>();
            _currentUserService = new Mock<ICurrentUserService>();
            _identityService = new Mock<IIdentityService>();
        }

        [TestMethod]
        public async Task OnLoggingBehavior_WithAuthenticadUser_CallsGetUserNameAsync()
        {
            //Arrange
            _currentUserService.Setup(x => x.UserId).Returns("Administrator");
            var requestLogger = new LoggingBehavior<CreateAssetCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            //Act
            await requestLogger.Process(new CreateAssetCommand(), new CancellationToken());

            //Assert
            _identityService.Verify( i=> i.GetUserNameAsync(It.IsAny<string>()), Times.Once );
        }

        [TestMethod]
        public async Task OnLoggingBehavior_WithUnauthenticadUser_CallsGetUserNameAsync()
        {
            //Arrange
            var requestLogger = new LoggingBehavior<CreateAssetCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

            //Act
            await requestLogger.Process(new CreateAssetCommand(), new CancellationToken());

            //Assert
            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }

    }
}
