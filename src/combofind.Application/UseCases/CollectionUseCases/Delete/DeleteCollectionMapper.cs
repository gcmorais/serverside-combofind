using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.CollectionUseCases.Delete
{
    public sealed class DeleteCollectionMapper : Profile
    {
        public DeleteCollectionMapper()
        {
            CreateMap<DeleteCollectionRequest, Collection>();
            CreateMap<Collection, CollectionResponse>();
        }
    }
}
