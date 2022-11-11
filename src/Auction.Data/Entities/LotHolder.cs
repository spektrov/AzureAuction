using Auction.Data.Entities;

namespace Auction.Data.Entities;

public class LotHolder : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid TariffId { get; set; }
    public Tariff? Tariff  { get; set; }
    
    public int? LotsLeft { get; set; }
}