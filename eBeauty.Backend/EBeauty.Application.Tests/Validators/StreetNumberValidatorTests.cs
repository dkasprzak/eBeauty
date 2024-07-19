using System.Text.RegularExpressions;
using EBeauty.Application.Helpers;
using EBeauty.Application.Validators;
using FluentValidation;
using Xunit.Abstractions;

namespace EBeauty.Application.Tests.Validators;

public class StreetNumberValidatorTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StreetNumberValidatorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("1")]
    [InlineData("3333")]
    [InlineData("3333/23 B")]
    [InlineData("12 C")]
    [InlineData("131")]
    [InlineData("1/2")]
    [InlineData("1/3 C")]
    [InlineData("131/1 B")]
    [InlineData(" 123   A ")]
    [InlineData("1122/1222")]
    [InlineData("15")]
    [InlineData("999/9999")]
    [InlineData("1234/56 A")]
    [InlineData("123 / 456 B")]
    [InlineData("123/456 B")]
    [InlineData("123 /456 B")]
    [InlineData("123/ 456 B")]
    [InlineData("123 / 456B")]
    [InlineData("123 /456B")]
    [InlineData("123/ 456B")]
    [InlineData("123 / 456  B")]
    [InlineData(" 123 / 456  B")]
    [InlineData(" 123 / 456  B  ")]
    [InlineData(" 123/ 456 B  ")]
    [InlineData(" 12 ")]
    public void StreetNumber_IsCorrect(string streetNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).StreetNumber();
        
        //Act
        var formattedStreetNumber = StreetNumberFormatter.FormatStreetNumber(streetNumber);
        var result = validator.Validate(formattedStreetNumber);
        
        //Assert
        Assert.True(result.IsValid);
        
        _testOutputHelper.WriteLine($"Original: {streetNumber}, Transformed: {formattedStreetNumber}");

    }

    [Theory]
    [InlineData("01")]
    [InlineData("12345")]
    [InlineData("1234/0")]
    [InlineData("12//34")]
    [InlineData("1234/56/78")]
    [InlineData("1234@A")]
    [InlineData("abcd")]
    [InlineData("/1234 A")]
    [InlineData("1234/")]
    [InlineData("133133/sds2121")]
    public void StreetNumber_IsIncorrect(string streetNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).StreetNumber();

        //Act
        var formattedStreetNumber = StreetNumberFormatter.FormatStreetNumber(streetNumber);
        var result = validator.Validate(formattedStreetNumber);

        //Assert
        Assert.False(result.IsValid);
    }
}
