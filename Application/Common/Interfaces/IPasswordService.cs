namespace Application.Common.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string password, string passwordHash);
}
