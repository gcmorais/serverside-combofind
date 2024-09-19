using combofind.Application.UseCases.CollectionUseCases.Create;
using FluentValidation;

namespace combofind.Application.UseCases.CollectionUseCases.Common
{
    public sealed class CollectionValidator : AbstractValidator<CreateCollectionRequest>
    {
        public CollectionValidator()
        {
            RuleFor(x => x.Color).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Budget).NotEmpty();
        }
    }
}
