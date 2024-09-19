using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Interface;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.GetAll
{
    public class GetAllCollectionHandler : IRequestHandler<GetAllCollectionRequest, List<CollectionResponse>>
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;

        public GetAllCollectionHandler(ICollectionRepository collectionRepository, IMapper mapper)
        {
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }

        public async Task<List<CollectionResponse>> Handle(GetAllCollectionRequest request, CancellationToken cancellationToken)
        {
            var collections = await _collectionRepository.GetAllCollections();

            return _mapper.Map<List<CollectionResponse>>(collections);
        }

    }
}
