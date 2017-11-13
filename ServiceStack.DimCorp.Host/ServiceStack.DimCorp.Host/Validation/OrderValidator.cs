using System;
using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.FluentValidation;

namespace ServiceStack.DimCorp.Host.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(r => r.CreationDate)
                .LessThan(DateTime.Now.AddSeconds(10))
                .WithMessage("Creation date shouldn't be in the future");

            RuleFor(r => r.Items)
                .NotEmpty()
                .WithMessage("Order Items sould be specified");
        }
    }
}