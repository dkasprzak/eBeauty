using EBeauty.Application.Validators;
using FluentValidation;

namespace EBeauty.Application.Tests.Validators;

public class StreetNumberValidatorTests
{
    [Theory]
    [InlineData("1")]
    [InlineData("3333")]
    [InlineData("3333/23 B")]
    [InlineData("12 C")]
    [InlineData("131")]
    [InlineData("1/2")]
    [InlineData("1/3 C")]
    [InlineData("131/1 B")]
    [InlineData("1122/1222")]
    [InlineData("15")]
    public void StreetNumber_IsCorrect(string streetNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).StreetNumber();
        
        //Act
        var result = validator.Validate(streetNumber);
        
        //Assert
        Assert.True(result.IsValid);
    }
}
