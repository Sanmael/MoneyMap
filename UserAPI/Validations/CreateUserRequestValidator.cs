using Business.Requests.User;
using FluentValidation;

namespace UserAPI.Validations
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must be less than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\!\@\#\$\%\^\&\*\(\)_\+\-=\[\]{};':\\|,.<>\/?]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is required.")
                .GreaterThanOrEqualTo(16).WithMessage("Age must be at least 16.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(x => x.Occupation)
                .MaximumLength(100).WithMessage("Occupation must be less than 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance must be equal or greater than 0.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(20).WithMessage("UserName must be less than 20 characters.");
        }
    }
}


