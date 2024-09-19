using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.CollectionUseCases.Update
{
    public sealed class UpdateCollectionMapper : Profile
    {
        public UpdateCollectionMapper()
        {
            CreateMap<UpdateCollectionRequest, Collection>();
            CreateMap<Collection, CollectionResponse>();
        }
    }
}
