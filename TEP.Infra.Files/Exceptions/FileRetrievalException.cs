﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Infra.Files.Exceptions
{
    public class FileRetrievalException : Exception
    {
        public FileRetrievalException(string message) : base(message)
        {
        }
    }
}
