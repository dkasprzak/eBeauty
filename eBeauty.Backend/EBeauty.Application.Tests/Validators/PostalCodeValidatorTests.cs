using EBeauty.Application.Validators;
using FluentValidation;

namespace EBeauty.Application.Tests.Validators;

public class PostalCodeValidatorTests
{
    [Theory]
    [InlineData("84-200")]
    [InlineData("67-100")]
    [InlineData("32-300")]
    [InlineData("34-500")]
    [InlineData("58-400")]
    [InlineData("64-820")]
    [InlineData("46-020")]
    [InlineData("30-001")]
    [InlineData("43-100")]
    [InlineData("51-128")]
    [InlineData("67-200")]
    [InlineData("84-240")]
    public void PostalCode_IsCorrect(string postalCode)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).PostalCode();

        //Act
        var result = validator.Validate(postalCode);

        //Assert
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [InlineData("84--200")]
    [InlineData("67-1300")]
    [InlineData("3-300")]
    [InlineData("34500")]
    [InlineData("583400")]
    [InlineData("6 24-820")]
    [InlineData("463dssw-020")]
    [InlineData("30-001asas")]
    [InlineData("43-as100")]
    [InlineData("51-12 28")]
    [InlineData("67-20 ed2$^&&^0")]
    [InlineData("84-2####40")]
    public void PostalCode_IsIncorrect(string postalCode)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).PostalCode();

        //Act
        var result = validator.Validate(postalCode);

        //Assert
        Assert.False(result.IsValid);
    }
}
