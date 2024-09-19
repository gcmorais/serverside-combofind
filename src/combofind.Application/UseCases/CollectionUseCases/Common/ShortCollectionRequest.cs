using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Common
{
    public sealed record ShortCollectionRequest(Guid Id) : IRequest<CollectionShortResponse>;
}
