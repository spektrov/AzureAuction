namespace Auction.Business.Responses;

public class ValidateRefreshTokenResponse : BaseResponse
{
    public Guid UserId { get; set; }
}