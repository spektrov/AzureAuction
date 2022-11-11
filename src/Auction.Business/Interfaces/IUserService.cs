using Auction.Business.Requests;
using Auction.Business.Responses;

namespace Auction.Business.Interfaces;

public interface IUserService
{
    Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
    
    Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
    
    Task<LogoutResponse> LogoutAsync(Guid userId);
    
    Task<UserResponse> GetInfoAsync(Guid userId);
}