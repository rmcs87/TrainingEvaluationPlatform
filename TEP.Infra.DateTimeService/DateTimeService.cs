using System;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;

namespace TEP.Infra.DateTimeService
{
    public class DateTimeService : IDateTimeService
    {
        public ServiceResponse<DateTime> GetCurrentTime()
        {
            return new ServiceResponse<DateTime> { Data = DateTime.Now, Success = true };
        }
    }
}
