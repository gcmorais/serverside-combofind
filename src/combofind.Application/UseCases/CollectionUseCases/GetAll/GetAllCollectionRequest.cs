using combofind.Application.UseCases.CollectionUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.GetAll
{
    public sealed record GetAllCollectionRequest : IRequest<List<CollectionResponse>>;
}
