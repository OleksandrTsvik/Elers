namespace Domain.Entities;

public class RefreshToken
{
    public static readonly int ExpirationDays = 7;

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresDate { get; set; }
    public DateTime? RevokedDate { get; set; }

    public User? User { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresDate;
    public bool IsActive => RevokedDate is null && !IsExpired;

    public RefreshToken()
    {
        ExpiresDate = DateTime.UtcNow.AddDays(ExpirationDays);
    }
}
