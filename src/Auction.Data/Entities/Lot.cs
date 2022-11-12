
namespace Auction.Data.Entities;

public class Lot : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public decimal StartPrice { get; set; }
    
    public decimal MaxPrice { get; set; }
    
    public DateTime TimeStart { get; set; }
    
    public DateTime TimeEnd { get; set; }
    
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}