﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Options;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService<FileAssetOptions> _fileHandler;

        public CreateAssetCommandHandler(IApplicationDbContext context, IFileService<FileAssetOptions> fileHandler)
        {
            _context = context;
            _fileHandler = fileHandler;
        }

        public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var imgPath = "path";   //Path will be generated by the FileHandler (ou melhor FileService?)

            var asset = new Asset(request.FilePath, request.Name, imgPath);

            _context.Assets.Add(asset);

            await _context.SaveChangesAsync(cancellationToken);

            return asset.Id;
        }
    }
}
