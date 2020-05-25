using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class OperatorApp : ServiceAppBase<Operator, OperatorDTO>, IOperatorApp
    {
        public OperatorApp(IServiceBase<Operator> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
