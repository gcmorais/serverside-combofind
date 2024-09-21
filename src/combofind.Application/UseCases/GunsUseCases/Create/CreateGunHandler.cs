using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using combofind.Resources;
using MediatR;

namespace combofind.Application.UseCases.GunsUseCases.Create
{
    public class CreateGunHandler : IRequestHandler<CreateGunRequest, GunResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGunsRepository _gunsRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public CreateGunHandler(
            IGunsRepository gunsRepository,
            ICollectionRepository collectionRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _gunsRepository = gunsRepository;
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }
        public async Task<GunResponse> Handle(CreateGunRequest request, CancellationToken cancellationToken)
        {
            var collection = await _collectionRepository.GetById(request.CollectionData.Id);

            if (collection == null)
            {
                throw new InvalidOperationException(ResourceErrorMessages.NotFound);
            }

            var collectionData = _mapper.Map<Guns>(request);

            collectionData.AssignCollection(collection);

            _gunsRepository.Create(collectionData);

            await _unitOfWork.Commit();
            return _mapper.Map<GunResponse>(collectionData);
        }
    }
}
