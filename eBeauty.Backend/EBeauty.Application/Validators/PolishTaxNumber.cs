using System.Text.RegularExpressions;
using FluentValidation;

namespace EBeauty.Application.Validators;

public static class PolishTaxNumberExtensionClass
{
    public static IRuleBuilderOptions<T, string> PolishTaxNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(BeAValidTaxNumber)
            .WithErrorCode("TaxNumberValidator");
    }

    private static bool BeAValidTaxNumber(string nip)
    {
        if (string.IsNullOrEmpty(nip) || nip.Length != 10 || !nip.All(char.IsDigit))
        {
            return false;
        }

        int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
        var checksum = 0;

        for (int i = 0; i < 9; i++)
        {
            checksum += (nip[i] - '0') * weights[i];
        }

        var controlDigit = checksum % 11;
        if (controlDigit == 10)
        {
            controlDigit = 0;
        }

        return controlDigit == (nip[9] - '0');

    }
}
