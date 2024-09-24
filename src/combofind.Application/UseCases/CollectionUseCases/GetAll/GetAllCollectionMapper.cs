using AutoMapper;
using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.CollectionUseCases.GetAll
{
    public sealed class GetAllCollectionMapper : Profile
    {
        public GetAllCollectionMapper()
        {
            CreateMap<Collection, CollectionResponse>()
            .ForMember(dest => dest.Guns, opt => opt.MapFrom(src => src.Guns));
            CreateMap<GunEntity, GunResponse>();
        }
    }
}
