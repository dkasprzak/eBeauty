using EBeauty.Application.Validators;
using FluentValidation;

namespace EBeauty.Application.Tests.Validators;

public class PolishTaxNumberValidatorTests
{
    
    [Theory]
    [InlineData("1171663771")]
    [InlineData("5354061131")]
    [InlineData("8384885536")]
    [InlineData("5299616913")]
    [InlineData("1079120036")]
    [InlineData("3817277904")]
    [InlineData("1234879803")]
    [InlineData("1214585381")]
    [InlineData("9711212994")]
    [InlineData("3414801175")]
    public void PolishTaxNumber_IsCorrect(string taxNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).PolishTaxNumber();
        
        //Act
        var result = validator.Validate(taxNumber);
        
        //Assert
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [InlineData("1234567891")]
    [InlineData("2345678902")]
    [InlineData("3456789014")]
    [InlineData("4567890124")]
    [InlineData("5678901235")]
    [InlineData("6789012346")]
    [InlineData("7890123457")]
    [InlineData("8901234568")]
    [InlineData("9012345679")]
    [InlineData("0123456780")]
    public void PolishTaxNumber_IsIncorrect(string taxNumber)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).PolishTaxNumber();
        
        //Act
        var result = validator.Validate(taxNumber);
        
        //Assert
        Assert.False(result.IsValid);
    }

}
