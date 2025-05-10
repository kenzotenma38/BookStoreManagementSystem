using BookStore.Application.DTOs.Requests;
using FluentValidation;

namespace BookStore.Application.Validations
{
    public class CustomerRequestValidator : AbstractValidator<CustomerRequestDto>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}