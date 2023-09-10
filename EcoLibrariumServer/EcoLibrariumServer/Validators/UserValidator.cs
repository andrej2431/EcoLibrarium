using EcoLibrariumServer.Models;
using FluentValidation;


namespace EcoLibrariumServer.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {
            RuleFor(user => user).NotNull().WithMessage("User cannot be null.");

            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username has to be at least 5 characters long")
                .MaximumLength(15).WithMessage("Username has to be at most 15 characters long");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.PasswordHash)
                .NotEmpty().WithMessage("Password is required.");      
        }
    }
}
