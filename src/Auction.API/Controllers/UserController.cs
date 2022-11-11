using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Auction.Business.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UsersController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }
    
    [Authorize]
    [HttpGet]
    [Route("info")]
    public async Task<IActionResult> Info()
    {
        var userResponse = await _userService.GetInfoAsync(UserId);

        if (!userResponse.Success)
        {
            return UnprocessableEntity(userResponse);
        }

        return Ok(userResponse);

    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var loginResponse = await _userService.LoginAsync(loginRequest);

        if (!loginResponse.Success)
        {
            return Unauthorized(new
            {
                loginResponse.ErrorCode,
                loginResponse.Error
            });
        }

        return Ok(loginResponse);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        var validateRefreshTokenResponse = await _tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);

        if (!validateRefreshTokenResponse.Success)
        {
            return BadRequest(validateRefreshTokenResponse);
        }

        var tokenResponse = await _tokenService.GenerateTokensAsync(validateRefreshTokenResponse.UserId);

        return Ok(new TokenResponse { AccessToken = tokenResponse.Item1, RefreshToken = tokenResponse.Item2 });
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(SignupRequest signupRequest)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x =>
                x.Errors.Select(c => c.ErrorMessage)).ToList();
            if (errors.Any())
            {
                return BadRequest(new TokenResponse
                {
                    Error = $"{string.Join(",", errors)}",
                    ErrorCode = "S01"
                });
            }
        }
     
        var signupResponse = await _userService.SignupAsync(signupRequest);

        if (!signupResponse.Success)
        {
            return UnprocessableEntity(signupResponse);
        }

        return Ok(signupResponse.Email);
    }

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        var logout = await _userService.LogoutAsync(UserId);

        if (!logout.Success)
        {
            return UnprocessableEntity(logout);
        }

        return Ok();
    }
}