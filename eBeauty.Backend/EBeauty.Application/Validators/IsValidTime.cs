using System.Text.RegularExpressions;
using FluentValidation;

namespace EBeauty.Application.Validators;

public static class ValidTimeExtensionClass
{
    private static readonly Regex TimeRegex = new Regex(
        @"^(?:[01]?\d|2[0-3]):[0-5]?\d(?::[0-5]?\d)?$",
        RegexOptions.Compiled);

    public static IRuleBuilderOptions<T, string> IsValidTime<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(BeAValidTime)
            .WithMessage("ValidTimeValidator");
    }

    private static bool BeAValidTime(string time)
    {
        return TimeRegex.IsMatch(time) && TimeSpan.TryParse(time, out _);
    }
}