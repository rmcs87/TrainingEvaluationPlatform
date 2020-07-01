using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Application.Assets.Commands.DeleteAsset
{
    public class DeleteAssetCommand : IRequest
    {
        public int Id { get; set; }
    }
}
