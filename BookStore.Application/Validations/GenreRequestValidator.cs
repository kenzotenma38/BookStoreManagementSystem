using BookStore.Application.DTOs.Requests;
using FluentValidation;

namespace BookStore.Application.Validations
{
    public class GenreRequestValidator : AbstractValidator<GenreRequestDto>
    {
        public GenreRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}