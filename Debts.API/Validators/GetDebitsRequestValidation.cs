using Debts.API.Requests;
using FluentValidation;

namespace Debts.API.Validators
{
    public class GetDebitsRequestValidation : AbstractValidator<GetDebitsRequest>
    {
        public GetDebitsRequestValidation()
        {
            RuleFor(x => x).Must(x=> !string.IsNullOrEmpty(x.UserId.ToString())).WithMessage("O UserId é obrigatorio");
        }
    }
}