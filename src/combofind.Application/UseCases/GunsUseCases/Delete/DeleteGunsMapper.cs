using AutoMapper;
using combofind.Application.UseCases.GunsUseCases.Common;
using combofind.Domain.Entities;

namespace combofind.Application.UseCases.GunsUseCases.Delete
{
    public sealed class DeleteGunsMapper : Profile
    {
        public DeleteGunsMapper()
        {
            CreateMap<DeleteGunsRequest, GunEntity>();
            CreateMap<GunEntity, GunResponse>();
        }
    }
}
