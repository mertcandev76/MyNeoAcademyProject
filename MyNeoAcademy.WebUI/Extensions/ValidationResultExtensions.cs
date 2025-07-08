using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation.Results;

namespace MyNeoAcademy.WebUI.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void AddToModelState(this ValidationResult validationResult, ModelStateDictionary modelState, string? prefix = null)
        {
            foreach (var error in validationResult.Errors)
            {
                var key = string.IsNullOrEmpty(prefix) ? error.PropertyName : $"{prefix}.{error.PropertyName}";
                modelState.AddModelError(key, error.ErrorMessage);
            }
        }
    }
}
