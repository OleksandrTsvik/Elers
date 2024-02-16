using Application.Common.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Authentication;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BC.EnhancedHashPassword(password);
    }

    public bool VerifyHashedPassword(string password, string passwordHash)
    {
        return BC.EnhancedVerify(password, passwordHash);
    }
}
