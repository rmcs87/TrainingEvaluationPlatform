using System;
using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        ServiceResponse<DateTime> GetCurrentTime();
    }
}
