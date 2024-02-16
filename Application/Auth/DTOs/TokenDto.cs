namespace Application.Auth.DTOs;

public class TokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresDate { get; set; }
}
