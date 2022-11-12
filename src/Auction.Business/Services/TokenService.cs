using System.Security.Authentication;
using Auction.Business.Helpers;
using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Auction.Business.Responses;
using Auction.Data;
using Auction.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.Business.Services;

public class TokenService : ITokenService
{
    private readonly AuctionDbContext _auctionDbContext;
    
    public TokenService(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }

    public async Task<Tuple<string, string>> GenerateTokensAsync(Guid userId)
    {
        var accessToken = await TokenHelper.GenerateAccessToken(userId);
        var refreshToken = await TokenHelper.GenerateRefreshToken();

        var userRecord = await _auctionDbContext.Users.Include(o => o.RefreshTokens)
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (userRecord == null)
        {
            throw new AuthenticationException("Cannot find user by id.");
        }

        PasswordHelper.CreatePasswordHash(refreshToken, out byte[] hash, out byte[] salt);

        if (userRecord.RefreshTokens.Any())
        {
            await RemoveRefreshTokenAsync(userRecord);
        }
        
        userRecord.RefreshTokens.Add(new RefreshToken
        {
            ExpiryDate = DateTime.Now.AddDays(14),
            Ts = DateTime.Now,
            UserId = userId,
            TokenHash = Convert.ToBase64String(hash),
            TokenSalt = Convert.ToBase64String(salt)
        });

        await _auctionDbContext.SaveChangesAsync();

        var token = new Tuple<string, string>(accessToken, refreshToken);

        return token;
    }

    public async Task<bool> RemoveRefreshTokenAsync(User user)
    {
        var userRecord = await _auctionDbContext.Users.Include(o => o.RefreshTokens)
            .FirstOrDefaultAsync(e => e.Id == user.Id);

        if (userRecord == null)
        {
            return false;
        }

        if (!userRecord.RefreshTokens.Any()) return false;
        
        var currentRefreshToken = userRecord.RefreshTokens.First();

        _auctionDbContext.RefreshTokens.Remove(currentRefreshToken);

        return false;
    }

    public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        var refreshToken = await _auctionDbContext.RefreshTokens.FirstOrDefaultAsync(o => 
            o.UserId == Guid.Parse(refreshTokenRequest.UserId));

        var response = new ValidateRefreshTokenResponse();
        if (refreshToken == null)
        {
            response.Success = false;
            response.Error = "Invalid session or user is already logged out";
            response.ErrorCode = "invalid_grant";
            return response;
        }

        if (refreshTokenRequest.RefreshToken != null)
        {
           var verify =  PasswordHelper.VerifyPasswordHash(
                refreshTokenRequest.RefreshToken,
                Convert.FromBase64String(refreshToken.TokenHash),
                Convert.FromBase64String(refreshToken.TokenSalt));
           
            if (!verify)
            {
                response.Success = false;
                response.Error = "Invalid refresh token";
                response.ErrorCode = "invalid_grant";
                return response;
            }
        }

        if (refreshToken.ExpiryDate < DateTime.Now)
        {
            response.Success = false;
            response.Error = "Refresh token has expired";
            response.ErrorCode = "invalid_grant";
            return response;
        }

        response.Success = true;
        response.UserId = refreshToken.UserId;

        return response;
    }
}