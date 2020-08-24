using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        ServiceResponse<string> RecoverUserId { get; }
    }
}
