using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.GunsUseCases.Create
{
    public sealed class CreateGunMapper : Profile
    {
        public CreateGunMapper()
        {
            // Mapeia CreateGunRequest para Guns
            CreateMap<CreateGunRequest, GunEntity>()
                .ForMember(dest => dest.Collection, opt => opt.Ignore());
        }
    }
}
