using Auction.Business.Requests;
using Auction.Business.Responses;
using Auction.Data.Entities;

namespace Auction.Business.Interfaces;

public interface ITokenService
{
    Task<Tuple<string, string>> GenerateTokensAsync(Guid userId);
    
    Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    
    Task<bool> RemoveRefreshTokenAsync(User user);
}