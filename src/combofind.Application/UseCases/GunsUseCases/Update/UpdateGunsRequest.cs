using combofind.Application.UseCases.GunsUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.GunsUseCases.Update
{
    public sealed record UpdateGunsRequest(Guid Id, string Name, string Type, string Quality, string Class, string Condition, string MainColor, decimal AveragePrice, string Image) : IRequest<GunResponse>;
}
