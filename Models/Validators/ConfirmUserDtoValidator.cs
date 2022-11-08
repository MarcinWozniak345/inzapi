using Inzynierka_API.Entities;
using FluentValidation;
using Inzynierka_API.Models;

namespace APIpz.Models.Validators
{
    public class ConfirmUserDtoValidator : AbstractValidator<ConfirmUserDto>
    {
        public ConfirmUserDtoValidator(BazaDbContext context)
        {
            RuleFor(x => x.Kod).NotEmpty().Length(5);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
