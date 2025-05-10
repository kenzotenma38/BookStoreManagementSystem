using BookStore.Application.DTOs.Requests;
using FluentValidation;

namespace BookStore.Application.Validations
{
    public class BookRequestValidator : AbstractValidator<BookRequestDto>
    {
        public BookRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.AuthorId).GreaterThan(0);
            RuleFor(x => x.GenreId).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        }
    }
}