using combofind.Application.UseCases.CollectionUseCases.Common;
using combofind.Application.UseCases.GunsUseCases.Common;
using MediatR;

namespace combofind.Application.UseCases.GunsUseCases.Create
{
    public sealed record CreateGunRequest(string Name, string Type, string Quality, string Class, string Condition, string MainColor, decimal AveragePrice, string Image, ShortCollectionRequest CollectionData) : IRequest<GunResponse>;
}
