namespace Auction.Business.Requests;

public class BidRequest
{
    public Guid UserId { get; set; }
    
    public Guid LotId { get; set; }
    
    public DateTime Ts { get; set; }
    
    public decimal Price { get; set; }
}