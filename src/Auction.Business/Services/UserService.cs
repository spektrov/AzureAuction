using Auction.Business.Helpers;
using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Auction.Business.Responses;
using Auction.Data;
using Auction.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.Business.Services;

public class UserService : IUserService
{
    private readonly AuctionDbContext _auctionDbContext;
   private readonly ITokenService _tokenService;

    public UserService(AuctionDbContext auctionDbContext, ITokenService tokenService)
    {
        _auctionDbContext = auctionDbContext;
        _tokenService = tokenService;
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = _auctionDbContext.Users.SingleOrDefault(user => user.Active && user.Email == loginRequest.Email);

        if (user == null)
        {
            return new TokenResponse
            {
                Success = false,
                Error = "Email not found",
                ErrorCode = "L02"
            };
        }
        
        var verify = PasswordHelper.VerifyPasswordHash(
            loginRequest.Password, 
            Convert.FromBase64String(user.PasswordHash), 
            Convert.FromBase64String(user.PasswordSalt));

        if (!verify)
        {
            return new TokenResponse
            {
                Success = false,
                Error = "Invalid Password",
                ErrorCode = "L03"
            };
        }

        var token = await System.Threading.Tasks.Task.Run(() =>
            _tokenService.GenerateTokensAsync(user.Id));

        return new TokenResponse
        {
            Success = true,
            AccessToken = token.Item1,
            RefreshToken = token.Item2,
            UserId = user.Id,
            FirstName = user.FirstName
        };
    }

    public async Task<LogoutResponse> LogoutAsync(Guid userId)
    {
        var user = await _auctionDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        var removeResult = await _tokenService.RemoveRefreshTokenAsync(user!);

        return removeResult ?
            new LogoutResponse { Success = true } :
            new LogoutResponse { Success = false, Error = "Unable to logout user", ErrorCode = "L04" };
    }

    public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
    {
        var existingUser = await _auctionDbContext.Users.FirstOrDefaultAsync(user => user.Email == signupRequest.Email);

        if (existingUser != null)
        {
            return new SignupResponse
            {
                Success = false,
                Error = "User already exists with the same email",
                ErrorCode = "S02"
            };
        }

        if (signupRequest.Password != signupRequest.ConfirmPassword) {
            return new SignupResponse
            {
                Success = false,
                Error = "Password and confirm password do not match",
                ErrorCode = "S03"
            };
        }
        
        PasswordHelper.CreatePasswordHash(signupRequest.Password, out byte[]  passwordHash, out byte[]  passwordSalt);

        var user = new User
        {
            Email = signupRequest.Email,
            PasswordHash = Convert.ToBase64String(passwordHash) ,
            PasswordSalt = Convert.ToBase64String(passwordSalt),
            FirstName = signupRequest.FirstName,
            LastName = signupRequest.LastName,
            Ts = signupRequest.Ts,
            Active = true
        };

        await _auctionDbContext.Users.AddAsync(user);

        var saveResponse = await _auctionDbContext.SaveChangesAsync();

        if (saveResponse >= 0)
        {
            return new SignupResponse { Success = true, Email = user.Email };
        }

        return new SignupResponse
        {
            Success = false,
            Error = "Unable to save the user",
            ErrorCode = "S05"
        };
    }
    
    public async Task<UserResponse> GetInfoAsync(Guid userId)
    {
        var user = await _auctionDbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return new UserResponse
            {
                Success = false,
                Error = "No user found",
                ErrorCode = "I001"
            };
        }

        return new UserResponse
        {
            Success = true,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreationDate = user.Ts
        };
    }
}