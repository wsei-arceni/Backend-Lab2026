using System.Security.Claims;
using AppCore.Dto;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) =>
        _authService = authService;

    /// <summary>Logowanie — zwraca access token i refresh token.</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        return Ok(await _authService.LoginAsync(dto));
    }

    /// <summary>Odświeżenie access tokenu.</summary>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
    {
        return Ok(await _authService.RefreshTokenAsync(dto));
    }

    /// <summary>Wylogowanie — unieważnia refresh token.</summary>
    [HttpPost("revoke")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Revoke([FromBody] string refreshToken)
    {
        await _authService.RevokeTokenAsync(refreshToken);
        return NoContent();
    }

    /// <summary>Dane zalogowanego użytkownika.</summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(CrmUserDto), StatusCodes.Status200OK)]
    public IActionResult Me()
    {
        // Claims z tokenu — informacje o użytkowniku pobrane z tokenu
        // porównaj z kodem metody GenerateAccessToken w AuthService
        var user = new CrmUserDto
        {
            Id         = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            Email      = User.FindFirstValue(ClaimTypes.Email)!,
            FirstName  = User.FindFirstValue(ClaimTypes.GivenName)!,
            LastName   = User.FindFirstValue(ClaimTypes.Surname)!,
            Department = User.FindFirstValue("department")!,
            Roles      = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        };

        return Ok(user);
    }
}