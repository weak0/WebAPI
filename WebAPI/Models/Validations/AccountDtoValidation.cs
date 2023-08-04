using FluentValidation;
using WebAPI.Entities;

namespace WebAPI.Models.Validations
{
    public class AccountDtoValidation : AbstractValidator<AccountDto>
    {
        public AccountDtoValidation(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(z => z.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That emiail is taken");
                }
            });

        }
    }
}
