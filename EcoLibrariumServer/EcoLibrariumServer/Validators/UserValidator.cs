using EcoLibrariumServer.Models.Authentication;
using FluentValidation;


namespace EcoLibrariumServer.Validators
{
    public class RegisterUserCredentialsValidator : AbstractValidator<RegisterUserCredentials>
    {
        public RegisterUserCredentialsValidator() {
            RuleFor(user => user).NotNull().WithMessage("User cannot be null.");

            
            int minimumUsernameLength = 5, maximumUsernameLength = 15;

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(minimumUsernameLength).WithMessage($"Username has to be at least {minimumUsernameLength} characters long.")
                .MaximumLength(maximumUsernameLength).WithMessage($"Username has to be at most {maximumUsernameLength} characters long.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            int minimumPasswordLength = 8, maximumPasswordLength = 256;
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(minimumPasswordLength).WithMessage($"Password has to be at least {minimumPasswordLength} characters long.")
                .MaximumLength(maximumPasswordLength).WithMessage($"Password has to be at most {maximumPasswordLength} characters long.");
           

        }
    }

    public class LoginUserCredentialsValidator : AbstractValidator<LoginUserCredentials>
    {
        public LoginUserCredentialsValidator()
        {
            RuleFor(user => user).NotNull().WithMessage("User cannot be null.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
