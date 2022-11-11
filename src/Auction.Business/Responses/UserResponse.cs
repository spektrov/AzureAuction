namespace Auction.Business.Responses;

public class UserResponse : BaseResponse
{
    public string? Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}