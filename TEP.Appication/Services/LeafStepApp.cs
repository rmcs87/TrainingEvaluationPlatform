using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class LeafStepApp : StepApp
    {
        public LeafStepApp(IServiceBase<Step> service, IMapper iMapper) : base(service, iMapper)
        {
        }
    }
}
