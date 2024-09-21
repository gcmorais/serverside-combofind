using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Entities;
using combofind.Domain.Interface;
using combofind.Resources;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Create
{
    public class CreateCollectionHandler : IRequestHandler<CreateCollectionRequest, CollectionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public CreateCollectionHandler(
            ICollectionRepository collectionRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }
        public async Task<CollectionResponse> Handle(CreateCollectionRequest request, CancellationToken cancellationToken)
        {
            var existingColor = await _collectionRepository.GetByColor(request.Color);

            if (existingColor != null)
            {
                throw new InvalidOperationException(ResourceErrorMessages.ColorAlreadyExists);
            }

            var collection = new Collection(
                request.Color,
                request.Budget
            );

            _collectionRepository.Create(collection);

            await _unitOfWork.Commit();
            return _mapper.Map<CollectionResponse>(collection);
        }
    }
}
