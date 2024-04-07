namespace Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public DateTime? RevokedDate { get; set; }

    public User? User { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
    public bool IsActive => RevokedDate is null && !IsExpired;
}
