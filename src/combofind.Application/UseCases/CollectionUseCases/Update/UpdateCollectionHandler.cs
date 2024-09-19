using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Interface;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Update
{
    public class UpdateCollectionHandler : IRequestHandler<UpdateCollectionRequest, CollectionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public UpdateCollectionHandler(IUnitOfWork unitOfWork, ICollectionRepository collectionRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }

        public async Task<CollectionResponse> Handle(UpdateCollectionRequest request, CancellationToken cancellationToken)
        {
            var collection = await _collectionRepository.GetById(request.Id);

            if (collection == null)
            {
                return default;
            }

            if (!string.IsNullOrWhiteSpace(request.Budget))
            {
                collection.UpdateBudget(request.Budget);
            }

            if (!string.IsNullOrWhiteSpace(request.Color))
            {
                collection.UpdateColor(request.Color);
            }

            _collectionRepository.Update(collection);

            await _unitOfWork.Commit();

            return _mapper.Map<CollectionResponse>(collection);
        }
    }
}
