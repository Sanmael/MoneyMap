namespace Business.Validations.Messages
{
    public static class CommonValidationMessages
    {
        public const string RequiredField = "A Propriedade {0} é obrigatoria";        
        public const string InvalidValueField = "A Propriedade {0} não pode ser 0 nem negativa";

        public static string FormatMessage(string message, string field)
        {
            return string.Format(message, field);
        }
    }
}