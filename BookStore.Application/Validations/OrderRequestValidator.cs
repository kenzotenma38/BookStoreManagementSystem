using BookStore.Application.DTOs.Requests;
using FluentValidation;

namespace BookStore.Application.Validations
{
    public class OrderRequestValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.OrderDetails).NotEmpty();
            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailRequestValidator());
        }
    }

    public class OrderDetailRequestValidator : AbstractValidator<OrderDetailRequestDto>
    {
        public OrderDetailRequestValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}