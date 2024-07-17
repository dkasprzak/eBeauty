using System.Text.RegularExpressions;
using FluentValidation;

namespace EBeauty.Application.Validators;

public static class ExtensionClass
{
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must((rootObject, prop, context) =>
        {
            var regex = @"^\+\d{2} \d{3} \d{3} \d{3}$";
            return Regex.IsMatch(prop, regex);   
        })
            .WithErrorCode("PhoneNumberValidator");
    }
}
