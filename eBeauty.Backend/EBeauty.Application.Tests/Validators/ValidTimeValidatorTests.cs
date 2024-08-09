using EBeauty.Application.Validators;
using FluentValidation;

namespace EBeauty.Application.Tests.Validators;

public class ValidTimeValidatorTests
{
    [Theory]
    [InlineData("12:00:00")]
    [InlineData("12:00")]
    [InlineData("11:30:00")]
    [InlineData("11:30")]
    [InlineData("02:55:00")]
    [InlineData("12:01")]
    [InlineData("18:01:56")]
    [InlineData("08:00")]
    [InlineData("07:30:00")]
    [InlineData("12:34")]
    [InlineData("19:21:00")]
    [InlineData("05:00")]
    [InlineData("01:00:00")]
    [InlineData("17:15")]
    public void Time_IsValid(string time)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).ValidTime();

        //Act
        var result = validator.Validate(time);

        //Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("12:99:12")]   // Minuty poza zakresem (99)
    [InlineData("99:11:00")]   // Godziny poza zakresem (99)
    [InlineData("50:30:11")]   // Godziny poza zakresem (50)
    [InlineData("24:00:00")]   // 24 nie jest poprawną godziną (powinno być 00:00:00)
    [InlineData("12:60:00")]   // Minuty poza zakresem (60)
    [InlineData("23:59:60")]   // Sekundy poza zakresem (60)
    [InlineData("25:01")]      // Godziny poza zakresem (25)
    [InlineData("00:00:61")]   // Sekundy poza zakresem (61)
    [InlineData("15:100:00")]  // Minuty mają więcej niż 2 cyfry
    [InlineData("99:99:99")]   // Wszystkie części czasu poza zakresem
    [InlineData("12:34:56:78")] // Za dużo sekcji czasu
    public void Time_IsInvalid(string time)
    {
        //Arrange
        var validator = new InlineValidator<string>();
        validator.RuleFor(x => x).ValidTime();
        
        //Act
        var result = validator.Validate(time);
        
        //Assert
        Assert.False(result.IsValid);
    }
}
