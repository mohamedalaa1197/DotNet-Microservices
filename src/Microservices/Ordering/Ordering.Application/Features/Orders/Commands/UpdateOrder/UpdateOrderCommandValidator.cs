using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(r => r.UserName)
                 .NotEmpty().WithMessage("UserName can't be empty")
                 .NotNull()
                 .MaximumLength(50).WithMessage("UserName Max. length is 50");

            RuleFor(r => r.EmailAddress)
                .NotEmpty().WithMessage("Email can't be empty");

            RuleFor(r => r.TotalPrice)
                .NotEmpty().WithMessage("Total price can't be empty")
                .GreaterThan(0).WithMessage("Total price should be greater than 0");
        }
    }
}
