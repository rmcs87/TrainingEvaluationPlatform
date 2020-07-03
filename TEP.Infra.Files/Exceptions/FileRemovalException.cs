using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Infra.Files.Exceptions
{
    public class FileRemovalException : Exception
    {
        public FileRemovalException(string message) : base(message)
        {
        }
    }
}
