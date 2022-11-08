using Inzynierka_API.Entities;
using FluentValidation;
using Inzynierka_API.Models;

namespace APIpz.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BazaDbContext context)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.HasloHash).MinimumLength(6);
            RuleFor(x => x.PotwierdzHaslo).Equal(e => e.HasloHash);
            RuleFor(x => x.Email).Custom((wartosc, kontekst) =>
                {
                    var CzyEmailJestwBazie = context.Users.Any(u => u.Email == wartosc);
                    if (CzyEmailJestwBazie)
                    {
                        kontekst.AddFailure("Email", "Ten email jest juz uzyty");
                    }
                });
            RuleFor(x => x.Login).Custom((wartosc, kontekst) =>
            {
                var CzyLoginJestwBazie = context.Users.Any(u => u.Login == wartosc);
                if (CzyLoginJestwBazie)
                {
                    kontekst.AddFailure("Login", "Ten Login jest juz uzyty");
                }
            });
        }
    }
}
