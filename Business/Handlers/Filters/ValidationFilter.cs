using FluentValidation;
using Microsoft.AspNetCore.Http;

public class ValidationFilter(IServiceProvider serviceProvider) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.FirstOrDefault();
        if (request == null)
            return await next(context);

        Type validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
        object? validator = serviceProvider.GetService(validatorType);

        if (validator is IValidator validatorInstance)
        {
            var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(request));

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Results.BadRequest(errors);
            }
        }

        return await next(context);
    }
}