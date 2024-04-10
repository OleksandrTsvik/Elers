using API.Constants;
using Application.Auth.DTOs;
using Application.Auth.GetInfo;
using Application.Auth.Login;
using Application.Auth.Logout;
using Application.Auth.Register;
using Application.Auth.UpdateToken;
using Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : ApiControllerBase
{
    private const bool useCookiesByDefault = true;

    [HttpGet("info")]
    public async Task<IActionResult> Info(CancellationToken cancellationToken)
    {
        var query = new GetInfoQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken,
        [FromQuery] bool useCookies = useCookiesByDefault)
    {
        var command = new LoginCommand(request.Email, request.Password);

        return HandleAuthResult(useCookies, await Sender.Send(command, cancellationToken));
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken,
        [FromQuery] bool useCookies = useCookiesByDefault)
    {
        var command = new RegisterCommand(request.Email, request.Password);

        return HandleAuthResult(useCookies, await Sender.Send(command, cancellationToken));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken,
        [FromQuery] bool useCookies = useCookiesByDefault)
    {
        string? refreshToken = request.RefreshToken;

        if (useCookies)
        {
            refreshToken = GetRefreshTokenFromCookie();
            RemoveAuthCookies();
        }

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Unauthorized();
        }

        var command = new LogoutCommand(refreshToken);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] UpdateTokenRequest request,
        CancellationToken cancellationToken,
        [FromQuery] bool useCookies = useCookiesByDefault)
    {
        string? refreshToken = useCookies
            ? GetRefreshTokenFromCookie()
            : request.RefreshToken;

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Unauthorized();
        }

        var command = new UpdateTokenCommand(refreshToken);

        return HandleAuthResult(useCookies, await Sender.Send(command, cancellationToken));
    }

    [NonAction]
    private IActionResult HandleAuthResult(bool useCookies, Result<AuthDto> result)
    {
        return useCookies
            ? HandleAuthResultWithCookies(result)
            : HandleResult(result);
    }

    [NonAction]
    private IActionResult HandleAuthResultWithCookies(Result<AuthDto> result)
    {
        if (result is null)
        {
            return GetErrorResult(Error.NullResult);
        }

        if (result.IsFailure)
        {
            return GetErrorResult(result.Error);
        }

        if (result.Value is null)
        {
            return Unauthorized();
        }

        SetAuthCookies(result.Value.AccessToken, result.Value.RefreshToken);

        return Ok(result.Value.User);
    }

    [NonAction]
    private string? GetRefreshTokenFromCookie()
    {
        if (Request.Cookies.TryGetValue(Auth.CookieRefreshTokenKey, out string? refreshToken))
        {
            return refreshToken;
        }

        return null;
    }

    [NonAction]
    private void SetAuthCookies(TokenDto accessToken, TokenDto refreshToken)
    {
        Response.Cookies.Append(
            Auth.CookieAccessTokenKey,
            accessToken.Token,
            new CookieOptions()
            {
                HttpOnly = true,
                Expires = accessToken.ExpiresDate
            });

        Response.Cookies.Append(
            Auth.CookieRefreshTokenKey,
            refreshToken.Token,
            new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresDate
            });
    }

    [NonAction]
    private void RemoveAuthCookies()
    {
        Response.Cookies.Delete(Auth.CookieAccessTokenKey);
        Response.Cookies.Delete(Auth.CookieRefreshTokenKey);
    }
}
