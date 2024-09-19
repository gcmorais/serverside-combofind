using combofind.Application.UseCases.CollectionUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Update
{
    public sealed record UpdateCollectionRequest(Guid Id, string Color, string Budget) : IRequest<CollectionResponse>;

}
