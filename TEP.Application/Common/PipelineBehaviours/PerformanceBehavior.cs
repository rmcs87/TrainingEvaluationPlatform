using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;

namespace TEP.Application.Common.PipelineBehaviours
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private const int boundaryMillisecondsTime = 500;

        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public PerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var resposnse = await next();

            _timer.Stop();

            var elapasedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapasedMilliseconds > boundaryMillisecondsTime)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _currentUserService.UserId ?? string.Empty;
                var userName = string.Empty;

                if (!string.IsNullOrEmpty(userId))
                {
                    userName = await _identityService.GetUserNameAsync(userId);
                }

                _logger.LogWarning("TEP Long Running Request: {Name} ({ElapsedMiliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapasedMilliseconds, userId, userName, request);
            }

            return resposnse;
        }
    }
}
