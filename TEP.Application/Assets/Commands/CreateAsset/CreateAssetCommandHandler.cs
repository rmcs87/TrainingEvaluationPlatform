using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TEP.Application.Common.Interfaces;
using TEP.Application.Common.Models;
using TEP.Application.Common.Options;
using TEP.Domain.Entities;

namespace TEP.Application.Assets.Commands.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CreateAssetCommandHandler(IApplicationDbContext context, IFileServiceFactory fileFactory, IMapper mapper)
        {
            _context = context;
            _fileService = fileFactory.Create<FileAssetOptions>();
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            string imgPath = "";
            try
            {
                ServiceResponse<string> response = await _fileService.SaveFile(request.Image);

                if (!response.Success)
                    throw new CreateAssetCommandException(response.Message);

                imgPath = response.Data;
                var asset = _mapper.Map<Asset>(request);
                asset.UpdateIcon(imgPath);

                _context.Assets.Add(asset);

                await _context.SaveChangesAsync(cancellationToken);

                return asset.Id;
            }
            catch (CreateAssetCommandException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    _fileService.RemoveFile(imgPath);
                }
                throw new CreateAssetCommandException("Asset could not be created due to Unknown Problems.");

            }
        }
    }
}
