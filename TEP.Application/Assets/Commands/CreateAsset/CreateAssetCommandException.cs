using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandException : Exception
    {
        public CreateAssetCommandException(string message) : base(message)
        {
        }

        public CreateAssetCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
