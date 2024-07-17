using System.Text.RegularExpressions;
using FluentValidation;

namespace EBeauty.Application.Validators;

public static class StreetNumberExtensionClass
{
    public static IRuleBuilderOptions<T, string> StreetNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must((rootObject, prop, context) =>
        {
            var regex = @"^[1-9]\d{0,3}( [a-zA-Z]|\/[1-9]\d{0,3} [a-zA-Z]?)?$";
            return Regex.IsMatch(prop, regex);   
        })
            .WithErrorCode("StreetNumberValidator");
    }
}
