using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.GunsUseCases.Update
{
    public sealed class UpdateGunsMapper : Profile
    {
        public UpdateGunsMapper()
        {
            CreateMap<UpdateGunsRequest, GunEntity>();
            CreateMap<GunEntity, GunResponse>();
        }
    }
}
