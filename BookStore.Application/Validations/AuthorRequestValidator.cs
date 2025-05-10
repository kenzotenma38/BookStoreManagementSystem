using BookStore.Application.DTOs.Requests;
using FluentValidation;

namespace BookStore.Application.Validations
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequestDto>
    {
        public AuthorRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}