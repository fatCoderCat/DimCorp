using ServiceStack.DimCorp.Host.ServiceModel;
using ServiceStack.FluentValidation;

namespace ServiceStack.DimCorp.Host.Validation
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            var nameNotSpeifiedMessage = "Name has not been specified";
            var maxLengthMsg = "Name can't be linger than 50 charaters";

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(nameNotSpeifiedMessage)
                .NotNull().WithMessage(nameNotSpeifiedMessage)
                .Length(1, 50).WithMessage(maxLengthMsg);
        }
    }
}