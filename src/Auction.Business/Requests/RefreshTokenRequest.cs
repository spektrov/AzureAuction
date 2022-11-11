using System.ComponentModel.DataAnnotations;

namespace Auction.Business.Requests;

public class RefreshTokenRequest
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string? RefreshToken { get; set; }
}