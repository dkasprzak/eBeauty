using System.Text.RegularExpressions;

namespace EBeauty.Application.Helpers;

public static class StreetNumberFormatter
{
    public static string FormatStreetNumber(string streetNumber)
    {
        streetNumber = Regex.Replace(streetNumber, @"\s+", " ").Trim();

        streetNumber = Regex.Replace(streetNumber, @"\s*/\s*", "/");

        streetNumber = Regex.Replace(streetNumber, @"([1-9]\d{0,3})([a-zA-Z])$", "$1 $2");

        streetNumber = Regex.Replace(streetNumber, @"(\d)(/)([a-zA-Z])", "$1/$3");

        streetNumber = streetNumber.Trim();

        return streetNumber;
    }
}
