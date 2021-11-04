using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Models.Validertors
{
    public class RegisterDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.CofirmedPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailUse = dbContext.Users.Any(u => u.Email == value);
                if(emailUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
            
        }
    }
}
