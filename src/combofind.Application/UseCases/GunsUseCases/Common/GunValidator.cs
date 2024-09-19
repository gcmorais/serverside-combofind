using combofind.Application.UseCases.GunsUseCases.Create;
using FluentValidation;

namespace combofind.Application.UseCases.GunsUseCases.Common
{
    public sealed class GunValidator : AbstractValidator<CreateGunRequest>
    {
        public GunValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
            RuleFor(x => x.Type).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Quality).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Class).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Condition).NotEmpty().MaximumLength(20);
            RuleFor(x => x.MainColor).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Image).NotEmpty();
        }
    }
}
