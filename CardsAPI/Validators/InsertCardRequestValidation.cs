using Business.Requests.Card;
using Business.Validations.Messages;
using FluentValidation;

namespace CardsAPI.Validators
{
    public static class InsertCardRequestValidationMessages
    {
        public const string InvalidLimit = "O limite não pode ser 0 nem negativo";
        public const string InvalidDueDate = "Não é possivel adicionar um cartão vencido";
    }

    public class InsertCardRequestValidation : AbstractValidator<InsertCardRequest>
    {
        public InsertCardRequestValidation()
        {
            RuleFor(card => card.UserId).Must(userId => !string.IsNullOrEmpty(userId.ToString())).WithMessage(CommonValidationMessages.FormatMessage(CommonValidationMessages.InvalidValueField, nameof(InsertCardRequest.UserId)));
            RuleFor(card => card.CategorieId).Must(categorieId => !string.IsNullOrEmpty(categorieId.ToString())).WithMessage(CommonValidationMessages.FormatMessage(CommonValidationMessages.InvalidValueField, nameof(InsertCardRequest.CategorieId)));
            RuleFor(card => card.DueDate).Must(dueDate => dueDate > DateTime.Now).WithMessage(InsertCardRequestValidationMessages.InvalidDueDate);
            RuleFor(card => card.Name).Must(name => !string.IsNullOrEmpty(name)).WithMessage(CommonValidationMessages.FormatMessage(CommonValidationMessages.RequiredField, nameof(InsertCardRequest.Name)));
            RuleFor(card => card.Limit).Must(limit => limit > 0).WithMessage(InsertCardRequestValidationMessages.InvalidLimit);
            RuleFor(card => card.Balance).Must(balance => balance >= 0).WithMessage(CommonValidationMessages.FormatMessage(CommonValidationMessages.InvalidValueField, nameof(InsertCardRequest.Balance)));
        }
    }
}