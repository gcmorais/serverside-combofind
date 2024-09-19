using combofind.Application.UseCases.CollectionUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.CollectionUseCases.Create
{
    public sealed record CreateCollectionRequest : IRequest<CollectionResponse>
    {
        public string Color { get; set; }
        public string Budget { get; set; }
    }
}
