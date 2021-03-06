﻿using TEP.Application.Common.Models;

namespace TEP.Application.Common.Interfaces
{
    public interface IFileServiceFactory
    {
        IFileService Create<FileOption>() where FileOption : IFileOptions, new();
    }
}
