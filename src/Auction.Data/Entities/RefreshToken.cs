namespace Auction.Data.Entities;

public class RefreshToken : BaseEntity
{
    public string TokenHash { get; set; }

    public string TokenSalt { get; set; }

    public DateTime Ts { get; set; }

    public DateTime ExpiryDate { get; set; }

    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}