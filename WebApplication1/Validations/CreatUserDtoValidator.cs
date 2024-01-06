using CoreLayer.DTOs;
using FluentValidation;

namespace AuthServer.API.Validations
{
    public class CreatUserDtoValidator : AbstractValidator<CreatUserDto>
    {
        public CreatUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is wrong");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
