namespace Auction.Data.Entities;

public class Task : BaseEntity
{
    public string? Name { get; set; } = string.Empty;
    
    public bool IsCompleted { get; set; }
    
    public DateTime Ts { get; set; }
    
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

}