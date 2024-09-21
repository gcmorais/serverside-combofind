using combofind.Application.UseCases.GunsUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.GunsUseCases.Delete
{
    public sealed record DeleteGunsRequest(Guid Id) : IRequest<GunResponse>;
}
