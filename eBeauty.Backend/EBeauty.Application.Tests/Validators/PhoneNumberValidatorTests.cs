using EBeauty.Application.Validators;
using FluentValidation;

namespace EBeauty.Application.Tests.Validators;

public class PhoneNumberValidatorTests
{
    [Theory]
    [InlineData("+48 123 123 123")]
    [InlineData("+48 223 123 123")]
    [InlineData("+48 423 123 123")]
    [InlineData("+48 523 123 123")]
    [InlineData("+48 623 123 123")]
    [InlineData("+48 823 123 123")]
    public void PhoneNumber_IsCorrect(string phoneNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).PhoneNumber();
        
        //Act
        var result = validator.Validate(phoneNumber);
        
        //Assert
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [InlineData("+48 12 123 123")]
    [InlineData("+48 22 123 23")]
    [InlineData("+696+48 423 123 123")]
    [InlineData("+48 5wdds23 123 123")]
    [InlineData("+48 623 d123 123")]
    [InlineData("823 123 123")]
    public void PhoneNumber_IsIncorrect(string phoneNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).PhoneNumber();
        
        //Act
        var result = validator.Validate(phoneNumber);
        
        //Assert
        Assert.False(result.IsValid);
    }
}
