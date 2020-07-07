using System;
using TEP.Application.Common.Interfaces;

namespace TEP.Infra.DateTimeService
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
