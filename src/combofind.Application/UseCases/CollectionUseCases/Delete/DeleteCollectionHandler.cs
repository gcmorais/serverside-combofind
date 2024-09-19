using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Interface;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Delete
{
    public class DeleteCollectionHandler : IRequestHandler<DeleteCollectionRequest, CollectionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public DeleteCollectionHandler(IUnitOfWork unitOfWork, ICollectionRepository collectionRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }
        public async Task<CollectionResponse> Handle(DeleteCollectionRequest request, CancellationToken cancellationToken)
        {
            var collection = await _collectionRepository.GetById(request.Id);

            if (collection == null) return default;

            _collectionRepository.Delete(collection);
            await _unitOfWork.Commit();

            return _mapper.Map<CollectionResponse>(collection);
        }
    }
}
