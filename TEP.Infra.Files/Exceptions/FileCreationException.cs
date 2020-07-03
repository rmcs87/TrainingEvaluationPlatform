using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Infra.Files.Exceptions
{
    public class FileCreationException : Exception
    {
        private string v;

        public FileCreationException(string message) : base(message)
        {
        }

        public FileCreationException(string message, string v) : this(message)
        {
            this.v = v;
        }
    }
}
