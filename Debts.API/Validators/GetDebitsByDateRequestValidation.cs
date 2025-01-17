using Debts.API.Requests;
using FluentValidation;

namespace Debts.API.Validators
{
    public class GetDebitsByDateRequestValidation : AbstractValidator<GetDebitsByDateRequest>
    {
        public GetDebitsByDateRequestValidation()
        {
            RuleFor(x => x).Must(x => x.StartMonth != null || x.EndMonth != null).WithMessage("É obrigatorio ao menos um filtro de Data");
        }
    }
}