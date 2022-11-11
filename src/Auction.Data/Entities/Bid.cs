namespace Auction.Data.Entities;

public class Bid : BaseEntity
{
    public Guid LotId { get; set; }
    public Lot? Lot { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime Time { get; set; }
}