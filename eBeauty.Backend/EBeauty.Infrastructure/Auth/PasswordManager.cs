using EBeauty.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EBeauty.Infrastructure.Auth;

public class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<DummyUser> _passwordHasher;

    public PasswordManager(IPasswordHasher<DummyUser> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(new DummyUser(), password);
    }

    public bool VerifyPassword(string hash, string password)
    {
        var verificationResult = _passwordHasher.VerifyHashedPassword(new DummyUser(), hash, password);
        if (verificationResult == PasswordVerificationResult.Success || verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            return true;
        }
        return false;
    }
    
    public class DummyUser;
    
}
