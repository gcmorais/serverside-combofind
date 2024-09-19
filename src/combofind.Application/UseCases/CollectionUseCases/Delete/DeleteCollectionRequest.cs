using combofind.Application.UseCases.CollectionUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Delete
{
    public sealed record DeleteCollectionRequest(Guid Id) : IRequest<CollectionResponse>;
}
