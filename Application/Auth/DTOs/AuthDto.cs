namespace Application.Auth.DTOs;

public class AuthDto
{
    public required AuthUserDto User { get; set; }
    public required TokenDto AccessToken { get; set; }
    public required TokenDto RefreshToken { get; set; }
}
