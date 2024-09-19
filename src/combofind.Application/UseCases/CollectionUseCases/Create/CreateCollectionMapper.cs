using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.CollectionUseCases.Create
{
    public sealed class CreateCollectionMapper : Profile
    {
        public CreateCollectionMapper()
        {
            CreateMap<CreateCollectionRequest, Collection>();
            CreateMap<Collection, CollectionResponse>();
        }
    }
}
